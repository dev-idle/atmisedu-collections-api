using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Handler for <see cref="GetAllCollectionArticlesDiscussionInteractionRequest"/>
    /// </summary>
    public class GetAllCollectionArticlesDiscussionInteractionHandler : IRequestHandler<GetAllCollectionArticlesDiscussionInteractionRequest, Result<List<CollectionArticlesDiscussionInteractionResponse>>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussionInteraction"/>>
        private readonly ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;

        /// <summary>
        /// Constructor of <see cref="GetAllCollectionArticlesDiscussionInteractionHandler"/>, inject needed dependency
        /// </summary>
        public GetAllCollectionArticlesDiscussionInteractionHandler(ICollectionArticlesDiscussionInteractionSqlRepository collectionArticlesDiscussionInteractionRepo, ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo)
        {
            this.collectionArticlesDiscussionInteractionRepo = collectionArticlesDiscussionInteractionRepo;
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
        }

        /// <summary>
        /// Handle <see cref="GetAllCollectionArticlesDiscussionInteractionRequest"/>, GetAll the <see cref="Entities.CollectionArticlesDiscussionInteraction"/> from database
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticlesDiscussionInteraction"/></returns>
        public async Task<Result<List<CollectionArticlesDiscussionInteractionResponse>>> Handle(GetAllCollectionArticlesDiscussionInteractionRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetAllCollectionArticlesDiscussionInteractionValidator();
            validator.ValidateAndThrow(request);

            // Default get records not deleted, filter by discussionId, articleId and isActived when they are sent
            var query = collectionArticlesDiscussionInteractionRepo.FindAll(x => x.IsActived != (int)ActivedType.DELETE);
            if (request.DiscussionId.HasValue)
                query = query.Where(x => x.DiscussionId == request.DiscussionId.Value);
            if (request.ArticleId.HasValue)
            {
                // Only get interactions of discussions belong to the article
                var discussionIds = collectionArticlesDiscussionRepo.FindAll(d => d.ArticleId == request.ArticleId.Value).Select(d => d.Id);
                query = query.Where(x => discussionIds.Contains(x.DiscussionId));
            }
            if (request.IsActived.HasValue)
                query = query.Where(x => x.IsActived == request.IsActived.Value);

            // Find CollectionArticlesDiscussionInteraction from database and return
            var data = query.OrderBy(x => x.Id).ToList().Select(CollectionArticlesDiscussionInteractionResponse.FromEntity).ToList();
            return Result<List<CollectionArticlesDiscussionInteractionResponse>>.Ok(data);
        }
    }
}
