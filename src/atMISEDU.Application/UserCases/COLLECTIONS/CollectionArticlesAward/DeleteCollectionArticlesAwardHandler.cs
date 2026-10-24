using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesAward;
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
    /// Handler for <see cref="DeleteCollectionArticlesAwardRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesAwardHandler : IRequestHandler<DeleteCollectionArticlesAwardRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesAward"/>>
        private readonly ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesAwardHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionArticlesAwardHandler(ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesAwardRepo = collectionArticlesAwardRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionArticlesAwardRequest"/>, soft delete the <see cref="Entities.CollectionArticlesAward"/> base on data <see cref="DeleteCollectionArticlesAwardRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionArticlesAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionArticlesAwardValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesAward (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesAward = await collectionArticlesAwardRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesAward == null || collectionArticlesAward.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesAward), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_AWARD_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesAward as Delete state
                collectionArticlesAward.IsActived = (int)ActivedType.DELETE;
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
