using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Handler for <see cref="DeleteCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionInteractionHandler : IRequestHandler<DeleteCollectionArticlesDiscussionInteractionRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionInteraction"/>>
        private readonly ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesDiscussionInteractionHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionArticlesDiscussionInteractionHandler(ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionInteractionRepo = collectionArticlesDiscussionInteractionRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionArticlesDiscussionInteractionRequest"/>, soft delete the <see cref="Entities.CollectionArticlesDiscussionInteraction"/> base on data <see cref="DeleteCollectionArticlesDiscussionInteractionRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionArticlesDiscussionInteractionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionArticlesDiscussionInteractionValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussionInteraction (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussionInteraction = await collectionArticlesDiscussionInteractionRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesDiscussionInteraction == null || collectionArticlesDiscussionInteraction.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussionInteraction), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_INTERACTION_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesDiscussionInteraction as Delete state
                collectionArticlesDiscussionInteraction.IsActived = (int)ActivedType.DELETE;
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
