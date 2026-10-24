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
    /// Handler for <see cref="UpdateCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionAwardHandler : IRequestHandler<UpdateCollectionArticlesDiscussionAwardRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionAward"/>>
        private readonly ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo;
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesDiscussionAwardHandler"/>, inject needed dependency
        /// </summary>
        public UpdateCollectionArticlesDiscussionAwardHandler(ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo,
                                                              ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo,
                                                              ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionAwardRepo = collectionArticlesDiscussionAwardRepo;
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="UpdateCollectionArticlesDiscussionAwardRequest"/>, Update the <see cref="Entities.CollectionArticlesDiscussionAward"/> base on data <see cref="UpdateCollectionArticlesDiscussionAwardRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(UpdateCollectionArticlesDiscussionAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new UpdateCollectionArticlesDiscussionAwardValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussionAward (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussionAward = await collectionArticlesDiscussionAwardRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesDiscussionAward == null || collectionArticlesDiscussionAward.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussionAward), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_AWARD_ID_NOT_FOUND);
            // Check CollectionAwardCatalog (not deleted) is exist when it is sent, throw NotFoundException when not found
            if (request.AwardId.HasValue
                && !await collectionAwardCatalogRepo.IsExistAsync(x => x.Id == request.AwardId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionAwardCatalog), MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_ID_NOT_FOUND);
            // Only fields sent with value are updated, null fields keep old value
            request.MapTo(collectionArticlesDiscussionAward, true);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesDiscussionAward as Update state
                collectionArticlesDiscussionAwardRepo.Update(collectionArticlesDiscussionAward);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok();
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
