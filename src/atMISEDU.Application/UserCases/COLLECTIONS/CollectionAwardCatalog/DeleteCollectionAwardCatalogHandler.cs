using atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Handler for <see cref="DeleteCollectionAwardCatalogRequest"/>
    /// </summary>
    public class DeleteCollectionAwardCatalogHandler : IRequestHandler<DeleteCollectionAwardCatalogRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionAwardCatalog"/>>
        private readonly ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionAwardCatalogHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionAwardCatalogHandler(ICollectionAwardCatalogSqlRepository collectionAwardCatalogRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionAwardCatalogRepo = collectionAwardCatalogRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionAwardCatalogRequest"/>, soft delete the <see cref="Entities.CollectionAwardCatalog"/> base on data <see cref="DeleteCollectionAwardCatalogRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionAwardCatalogRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionAwardCatalogValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionAwardCatalog (not deleted) from database, throw NotFoundException when not found
            var collectionAwardCatalog = await collectionAwardCatalogRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionAwardCatalog == null || collectionAwardCatalog.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionAwardCatalog), MsgCode.ERR_COLLECTIONS_COLLECTION_AWARD_CATALOG_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionAwardCatalog as Delete state
                collectionAwardCatalog.IsActived = (int)ActivedType.DELETE;
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
