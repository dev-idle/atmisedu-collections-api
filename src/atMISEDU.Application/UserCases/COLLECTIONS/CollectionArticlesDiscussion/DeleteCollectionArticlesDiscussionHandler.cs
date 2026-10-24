using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Handler for <see cref="DeleteCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionHandler : IRequestHandler<DeleteCollectionArticlesDiscussionRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesDiscussionHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionArticlesDiscussionHandler(ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionArticlesDiscussionRequest"/>, soft delete the <see cref="Entities.CollectionArticlesDiscussion"/> base on data <see cref="DeleteCollectionArticlesDiscussionRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionArticlesDiscussionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionArticlesDiscussionValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussion (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussion = await collectionArticlesDiscussionRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesDiscussion == null || collectionArticlesDiscussion.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussion), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesDiscussion as Delete state
                collectionArticlesDiscussion.IsActived = (int)ActivedType.DELETE;
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
