using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionAward;
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
    /// Handler for <see cref="DeleteCollectionArticlesDiscussionAwardRequest"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionAwardHandler : IRequestHandler<DeleteCollectionArticlesDiscussionAwardRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionAward"/>>
        private readonly ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="DeleteCollectionArticlesDiscussionAwardHandler"/>, inject needed dependency
        /// </summary>
        public DeleteCollectionArticlesDiscussionAwardHandler(ICollectionArticlesDiscussionAwardSqlRepository collectionArticlesDiscussionAwardRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionAwardRepo = collectionArticlesDiscussionAwardRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="DeleteCollectionArticlesDiscussionAwardRequest"/>, soft delete the <see cref="Entities.CollectionArticlesDiscussionAward"/> base on data <see cref="DeleteCollectionArticlesDiscussionAwardRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(DeleteCollectionArticlesDiscussionAwardRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new DeleteCollectionArticlesDiscussionAwardValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussionAward (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussionAward = await collectionArticlesDiscussionAwardRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesDiscussionAward == null || collectionArticlesDiscussionAward.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussionAward), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_AWARD_ID_NOT_FOUND);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesDiscussionAward as Delete state
                collectionArticlesDiscussionAward.IsActived = (int)ActivedType.DELETE;
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
