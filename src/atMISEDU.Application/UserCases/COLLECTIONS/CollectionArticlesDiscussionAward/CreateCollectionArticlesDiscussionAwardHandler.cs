using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Handler for <see cref="CreateCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionAwardHandler : IRequestHandler<CreateCollectionArticlesDiscussionAwardRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionAward"/>>
        private readonly ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesDiscussionAwardHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionArticlesDiscussionAwardHandler(ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo,
                                                              ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo,
                                                              ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo,
                                                              ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionAwardRepo = collectionArticlesDiscussionAwardRepo;
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionArticlesDiscussionAwardRequest"/>, Create the <see cref="Entities.CollectionArticlesDiscussionAward"/> base on data <see cref="CreateCollectionArticlesDiscussionAwardRequest"/>
        /// and save to database. When same user already awarded same discussion (include deleted row, regardless of award type), reuse that row instead of creating new one
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created (or reused) <see cref="Entities.CollectionArticlesDiscussionAward"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionArticlesDiscussionAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new CreateCollectionArticlesDiscussionAwardValidator();
            validator.ValidateAndThrow(request);

            // Check CollectionArticlesDiscussion (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionArticlesDiscussionRepo.IsExistAsync(x => x.Id == request.DiscussionId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussion), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_ID_NOT_FOUND);
            // Check CollectionAwardCatalog (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionAwardCatalogRepo.IsExistAsync(x => x.Id == request.AwardId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionAwardCatalog), MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_ID_NOT_FOUND);

            // Find CollectionArticlesDiscussionAward of same user on same discussion (include deleted row, regardless of award type)
            var collectionArticlesDiscussionAward = await collectionArticlesDiscussionAwardRepo.FindSingleAsync(x => x.DiscussionId == request.DiscussionId && x.CreatedBy == request.CreatedBy, true, cancellationToken);
            var isNew = collectionArticlesDiscussionAward == null;
            if (collectionArticlesDiscussionAward == null)
            {
                // Not found, create new CollectionArticlesDiscussionAward from request, created date is set by backend
                collectionArticlesDiscussionAward = request.MapTo<Entities.CollectionArticlesDiscussionAward>()!;
                collectionArticlesDiscussionAward.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
            else
            {
                // Same user POST again, reuse existing row (keep its id, createdBy and createdDate), only change the award
                collectionArticlesDiscussionAward.AwardId = (int)request.AwardId;
            }
            // Apply default value of status for both new and reused row
            collectionArticlesDiscussionAward.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesDiscussionAward as Create state when it is new, otherwise as Update state
                if (isNew)
                    collectionArticlesDiscussionAwardRepo.Add(collectionArticlesDiscussionAward);
                else
                    collectionArticlesDiscussionAwardRepo.Update(collectionArticlesDiscussionAward);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { daId = collectionArticlesDiscussionAward.Id });
            }
            catch (Exception)
            {
                // Rollback transaction if any exception happened, then throw exception
                transaction.Rollback();
                throw;
            }
        }
    }
}
