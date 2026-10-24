using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Handler for <see cref="CreateCollectionArticlesAwardRequest"/>
    /// </summary>
    public class CreateCollectionArticlesAwardHandler : IRequestHandler<CreateCollectionArticlesAwardRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesAward"/>>
        private readonly ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesAwardHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionArticlesAwardHandler(ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo,
                                                    ICollectionArticlesSqlRepository collectionArticlesRepo,
                                                    ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo,
                                                    ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesAwardRepo = collectionArticlesAwardRepo;
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionArticlesAwardRequest"/>, Create the <see cref="Entities.CollectionArticlesAward"/> base on data <see cref="CreateCollectionArticlesAwardRequest"/>
        /// and save to database. When same user already awarded same article (include deleted row, regardless of award type), reuse that row instead of creating new one
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created (or reused) <see cref="Entities.CollectionArticlesAward"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionArticlesAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new CreateCollectionArticlesAwardValidator();
            validator.ValidateAndThrow(request);

            // Check CollectionArticles (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionArticlesRepo.IsExistAsync(x => x.Id == request.ArticleId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticles), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_ID_NOT_FOUND);
            // Check CollectionAwardCatalog (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionAwardCatalogRepo.IsExistAsync(x => x.Id == request.AwardId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionAwardCatalog), MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_ID_NOT_FOUND);

            // Find CollectionArticlesAward of same user on same article (include deleted row, regardless of award type)
            var collectionArticlesAward = await collectionArticlesAwardRepo.FindSingleAsync(x => x.ArticleId == request.ArticleId && x.CreatedBy == request.CreatedBy, true, cancellationToken);
            var isNew = collectionArticlesAward == null;
            if (collectionArticlesAward == null)
            {
                // Not found, create new CollectionArticlesAward from request, created date is set by backend
                collectionArticlesAward = request.MapTo<Entities.CollectionArticlesAward>()!;
                collectionArticlesAward.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
            else
            {
                // Same user POST again, reuse existing row (keep its id, createdBy and createdDate), only change the award
                collectionArticlesAward.AwardId = (int)request.AwardId;
            }
            // Apply default value of status for both new and reused row
            collectionArticlesAward.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesAward as Create state when it is new, otherwise as Update state
                if (isNew)
                    collectionArticlesAwardRepo.Add(collectionArticlesAward);
                else
                    collectionArticlesAwardRepo.Update(collectionArticlesAward);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { awId = collectionArticlesAward.Id });
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
