using atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticles;
using atMISEDU.Application.Responses.COLLECTIONS.CollectionTags;
using atMISEDU.Application.Validators.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using MediatR;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.UserCases.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Handler for <see cref="GetDetailCollectionArticlesRequest"/>
    /// </summary>
    public class GetDetailCollectionArticlesHandler : IRequestHandler<GetDetailCollectionArticlesRequest, Result<CollectionArticlesDetailResponse>>
    {
        // Repository handle data access of <see cref="Entities.CollectionArticles"/>>
        private readonly ICollectionArticlesSqlRepository collectionArticlesRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesTags"/>>
        private readonly ICollectionArticlesTagsSqlRepository collectionArticlesTagsRepo;
        // Repository handle data access of <see cref="Entities.CollectionTags"/>>
        private readonly ICollectionTagsSqlRepository collectionTagsRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesDiscussion"/>>
        private readonly ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesInteraction"/>>
        private readonly ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo;
        // Repository handle data access of <see cref="Entities.CollectionArticlesAward"/>>
        private readonly ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo;

        /// <summary>
        /// Constructor of <see cref="GetDetailCollectionArticlesHandler"/>, inject needed dependency
        /// </summary>
        public GetDetailCollectionArticlesHandler(ICollectionArticlesSqlRepository collectionArticlesRepo,
                                                  ICollectionArticlesTagsSqlRepository collectionArticlesTagsRepo,
                                                  ICollectionTagsSqlRepository collectionTagsRepo,
                                                  ICollectionArticlesDiscussionSqlRepository collectionArticlesDiscussionRepo,
                                                  ICollectionArticlesInteractionSqlRepository collectionArticlesInteractionRepo,
                                                  ICollectionArticlesAwardSqlRepository collectionArticlesAwardRepo)
        {
            this.collectionArticlesRepo = collectionArticlesRepo;
            this.collectionArticlesTagsRepo = collectionArticlesTagsRepo;
            this.collectionTagsRepo = collectionTagsRepo;
            this.collectionArticlesDiscussionRepo = collectionArticlesDiscussionRepo;
            this.collectionArticlesInteractionRepo = collectionArticlesInteractionRepo;
            this.collectionArticlesAwardRepo = collectionArticlesAwardRepo;
        }

        /// <summary>
        /// Handle <see cref="GetDetailCollectionArticlesRequest"/>, GetDetail the <see cref="Entities.CollectionArticles"/> from database
        /// with linked tags and counts of discussions, interactions, awards
        /// </summary>
        /// <param name="request">Request to handle</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Result{TModel}"/> with founded <see cref="Entities.CollectionArticles"/></returns>
        public async Task<Result<CollectionArticlesDetailResponse>> Handle(GetDetailCollectionArticlesRequest request, CancellationToken cancellationToken)
        {
            // Create validator and validate request
            var validator = new GetDetailCollectionArticlesValidator();
            validator.ValidateAndThrow(request);

            // Find CollectionArticles (not deleted) from database, throw NotFoundException when not found
            var collectionArticles = collectionArticlesRepo.FindAll(x => x.Id == request.Id && x.IsActived != (int)ActivedType.DELETE).FirstOrDefault();
            if (collectionArticles == null)
            {
                CustomException.ThrowNotFoundException(typeof(Entities.CollectionArticles), MsgCode.ERR_COLLECTIONS_COLLECTION_ARTICLES_ID_NOT_FOUND);
            }
            var articleId = collectionArticles.Id;

            // Find tags (not deleted) linked to CollectionArticles, ordered by keyTags
            var keyTags = collectionArticlesTagsRepo.FindAll(x => x.ArticleId == articleId).Select(x => x.KeyTags);
            var tags = collectionTagsRepo.FindAll(x => keyTags.Contains(x.Id) && x.IsActived != (int)ActivedType.DELETE)
                                         .OrderBy(x => x.Id).ToList().Select(CollectionTagsResponse.FromEntity).ToList();

            // Count actived discussions (comments and replies), interactions and awards of CollectionArticles
            var discussionCount = collectionArticlesDiscussionRepo.FindAll(x => x.ArticleId == articleId && x.IsActived == (int)ActivedType.ACTIVED).Count();
            var interactionCount = collectionArticlesInteractionRepo.FindAll(x => x.ArticleId == articleId && x.IsActived == (int)ActivedType.ACTIVED).Count();
            var awardCount = collectionArticlesAwardRepo.FindAll(x => x.ArticleId == articleId && x.IsActived == (int)ActivedType.ACTIVED).Count();

            var response = CollectionArticlesDetailResponse.FromEntity(collectionArticles, tags, discussionCount, interactionCount, awardCount);
            return Result<CollectionArticlesDetailResponse>.Ok(response);
        }
    }
}
