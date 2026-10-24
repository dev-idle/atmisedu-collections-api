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
    /// Handler for <see cref="CreateCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionHandler : IRequestHandler<CreateCollectionArticlesDiscussionRequest, Result<object>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Unit of work to handle transaction
        private readonly ISqlUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor of <see cref="CreateCollectionArticlesDiscussionHandler"/>, inject needed dependency
        /// </summary>
        public CreateCollectionArticlesDiscussionHandler(ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo, ICollectionArticlesSqlRepository collectionArticlesRepo, ISqlUnitOfWork unitOfWork)
        {
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Handle <see cref="CreateCollectionArticlesDiscussionRequest"/>, Create the <see cref="Entities.CollectionArticlesDiscussion"/> base on data <see cref="CreateCollectionArticlesDiscussionRequest"/>
        /// and save to database. A user can create many discussions (comments or replies), so it is always inserted as new row
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with id of created <see cref="Entities.CollectionArticlesDiscussion"/></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Result<object>> Handle(CreateCollectionArticlesDiscussionRequest request, CancellationToken cancellationToken)
        {
            // Trim string fields, then create validator and validate request
            request.DisContent = request.DisContent?.Trim();
            var validator = new CreateCollectionArticlesDiscussionValidator();
            validator.ValidateAndThrow(request);

            // Check CollectionArticles (not deleted) is exist, throw NotFoundException when not found
            if (!await collectionArticlesRepo.IsExistAsync(x => x.Id == request.ArticleId && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticles), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_ID_NOT_FOUND);
            // Check parent CollectionArticlesDiscussion (not deleted) is exist when it is sent, throw NotFoundException when not found
            if (request.DiscussionPid.HasValue
                && !await collectionArticlesDiscussionRepo.IsExistAsync(x => x.Id == request.DiscussionPid && x.IsActived != (int)ActivedType.DELETE, cancellationToken))
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticlesDiscussion), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_DISCUSSION_ID_NOT_FOUND);

            // Create new CollectionArticlesDiscussion from request, apply default values
            var collectionArticlesDiscussion = request.MapTo<Entities.CollectionArticlesDiscussion>();
            collectionArticlesDiscussion.CreatedDate = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            collectionArticlesDiscussion.IsActived = request.IsActived ?? (int)ActivedType.ACTIVED;

            // Begin transaction
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Marked CollectionArticlesDiscussion as Create state
                collectionArticlesDiscussionRepo.Add(collectionArticlesDiscussion);
                // Save data to database
                await unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                transaction.Commit();
                return Result<object>.Ok<object>(new { discussionId = collectionArticlesDiscussion.Id });
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
