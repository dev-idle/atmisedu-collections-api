using atMISEDU.Application.Responses.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Detail response data of <see cref="Entities.CollectionArticles"/>, contains linked tags and counts of discussions, interactions, awards
    /// </summary>
    public class CollectionArticlesDetailResponse : CollectionArticlesResponse
    {
        public List<CollectionTagsResponse> Tags { get; set; } = new();
        public int DiscussionCount { get; set; }
        public int InteractionCount { get; set; }
        public int AwardCount { get; set; }

        /// <summary>
        /// Create detail response from <see cref="Entities.CollectionArticles"/>, linked tags and counts
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <param name="tags">Tags (not deleted) linked to the article</param>
        /// <param name="discussionCount">Number of actived discussions (comments and replies) of the article</param>
        /// <param name="interactionCount">Number of actived interactions of the article</param>
        /// <param name="awardCount">Number of actived awards of the article</param>
        /// <returns>Detail response with same data of entity, tags and counts</returns>
        public static CollectionArticlesDetailResponse FromEntity(Entities.CollectionArticles entity, List<CollectionTagsResponse> tags, int discussionCount, int interactionCount, int awardCount)
        {
            var response = entity.MapTo<CollectionArticlesDetailResponse>()!;
            response.ArticleId = entity.Id;
            response.Tags = tags;
            response.DiscussionCount = discussionCount;
            response.InteractionCount = interactionCount;
            response.AwardCount = awardCount;
            return response;
        }
    }
}
