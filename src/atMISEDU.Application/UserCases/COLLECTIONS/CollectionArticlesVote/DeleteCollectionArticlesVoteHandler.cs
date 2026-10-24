using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Handler for <see cref="DeleteCollectionArticlesVoteRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesVoteHandler : IRequestHandler<DeleteCollectionArticlesVoteRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesVote"/>>
        private readonly ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesVoteHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionArticlesVoteHandler(ICollectionArticlesVoteSqlRepository collectionArticlesVoteRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesVoteRepo = collectionArticlesVoteRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionArticlesVoteRequest"/>, soft delete the <see cref="Entities.CollectionArticlesVote"/> base on data <see cref="DeleteCollectionArticlesVoteRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionArticlesVoteRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionArticlesVoteValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesVote (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesVote = await collectionArticlesVoteRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesVote == null || collectionArticlesVote.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesVote), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_VOTE_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesVote as Delete state
                collectionArticlesVote.IsActived = (int)ActivedType.DELETE;
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
