using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.DependencyInjection.Extensions;
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
    /// Handler for <see cref="UpdateCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionHandler : IRequestHandler<UpdateCollectionArticlesDiscussionRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="UpdateCollectionArticlesDiscussionHandler"/>, inject needed dependency
        /// </summary>
        public UpdateCollectionArticlesDiscussionHandler(ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="UpdateCollectionArticlesDiscussionRequest"/>, Update the <see cref="Entities.CollectionArticlesDiscussion"/> base on data <see cref="UpdateCollectionArticlesDiscussionRequest"/>
        /// and save to database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with success status</returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(UpdateCollectionArticlesDiscussionRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.DisContent = request.DisContent?.Trim();
            var validator = new UpdateCollectionArticlesDiscussionValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticlesDiscussion (not deleted) from database, throw NotFoundException when not found
            var collectionArticlesDiscussion = await collectionArticlesDiscussionRepo.FindByIdAsync((int)request.Id, true, cancellationToken);
            if (collectionArticlesDiscussion == null || collectionArticlesDiscussion.IsActived == (int)ActivedType.DELETE)
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussion), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_ID_NOT_FOUND);
            // Only fields sent with value are updated, null fields keep old value
            request.MapTo(collectionArticlesDiscussion, true);

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesDiscussion as Update state
                collectionArticlesDiscussionRepo.Update(collectionArticlesDiscussion);
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
