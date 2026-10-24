using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Handler for <see cref="DeleteCollectionArticlesRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesHandler : IRequestHandler<DeleteCollectionArticlesRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionArticlesHandler(ICollectionArticlesSqlRepository collectionArticlesRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionArticlesRequest"/>, soft delete the <see cref="Entities.CollectionArticles"/> base on data <see cref="DeleteCollectionArticlesRequest"/>
        /// and save to database. Links with tags are kept
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionArticlesRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionArticlesValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticles (not deleted) from database, throw NotFoundException when not found
            var collectionArticles = await collectionArticlesRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticles == null || collectionArticles.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticles), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticles as Delete state
                collectionArticles.IsActived = (int)ActivedType.DELETE;
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
