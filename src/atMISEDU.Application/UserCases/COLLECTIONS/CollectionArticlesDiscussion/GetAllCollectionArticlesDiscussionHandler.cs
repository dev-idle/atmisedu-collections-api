using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionArticlesDiscussionRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesDiscussionHandler : IRequestHandler<GetAllCollectionArticlesDiscussionRequest, Result<List<CollectionArticlesDiscussionResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionArticlesDiscussionHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionArticlesDiscussionHandler(ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo)
        {
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionArticlesDiscussionRequest"/>, GetAll the <see cref="Entities.CollectionArticlesDiscussion"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesDiscussion"/></returns>
        public async Task<Result<List<CollectionArticlesDiscussionResponse>>> Handle(GetAllCollectionArticlesDiscussionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionArticlesDiscussionValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by articleId, discussionPid and isActived when they are sent (combined with AND)
            var query = collectionArticlesDiscussionRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.ArticleId.HasValue)
                query = query.Where(x => x.ArticleId == request.ArticleId.Value);
            // When discussionPid is not sent, parent is not filtered (get both comments and replies)
            if (request.DiscussionPid.HasValue)
                query = query.Where(x => x.DiscussionPid == request.DiscussionPid.Value);
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionArticlesDiscussion from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionArticlesDiscussionResponse.FromEntity).ToList();
            return Result<List<CollectionArticlesDiscussionResponse>>.Ok(data);
        }
    }
}
