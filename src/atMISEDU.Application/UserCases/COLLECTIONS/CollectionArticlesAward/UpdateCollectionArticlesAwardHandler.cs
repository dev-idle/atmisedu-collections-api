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
    /// Handler for <see cref="UpdateCollectionArticlesAwardRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesAwardHandler : IRequestHandler<UpdateCollectionArticlesAwardRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesAward"/>>
        private readonly ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo;
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesAwardHandler"/>, inject needed dependency
        /// </summary>
        public UpdateCollectionArticlesAwardHandler(ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo,
                                                    ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo,
                                                    ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesAwardRepo = collectionArticlesAwardRepo;
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="UpdateCollectionArticlesAwardRequest"/>, Update the <see cref="Entities.CollectionArticlesAward"/> base on data <see cref="UpdateCollectionArticlesAwardRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(UpdateCollectionArticlesAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new UpdateCollectionArticlesAwardValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesAward (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesAward = await collectionArticlesAwardRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesAward == null || collectionArticlesAward.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesAward), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_ID_NOT_FOUND);
            // Check CollectionAwardCatalog (not deleted) is exist when it is sent, throw NotFoundException when not found
            if (request.AwardId.HasValue
                && !await collectionAwardCatalogRepo.IsExistAsync(x => x.Id == request.AwardId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionAwardCatalog), MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_ID_NOT_FOUND);
            // Only fields sent with value are updated, null fields keep old value
            request.MapTo(collectionArticlesAward, true);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesAward as Update state
                collectionArticlesAwardRepo.Update(collectionArticlesAward);
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
