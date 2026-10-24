using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Detail response data of <see cref="Entities.CollectionArticlesDiscussion"/>, include count of actived interactions and awards of the discussion
    /// </summary>
    public class CollectionArticlesDiscussionDetailResponse : CollectionArticlesDiscussionResponse
    {
        public int InteractionCount { get; set; }
        public int AwardCount { get; set; }

        /// <summary>
        /// Create detail response from <see cref="Entities.CollectionArticlesDiscussion"/> and its counts
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <param name="interactionCount">Number of actived interactions of the discussion</param>
        /// <param name="awardCount">Number of actived awards of the discussion</param>
        /// <returns>Detail response with same data of entity and counts</returns>
        public static CollectionArticlesDiscussionDetailResponse FromEntity(Entities.CollectionArticlesDiscussion entity, int interactionCount, int awardCount)
        {
            var response = entity.MapTo<CollectionArticlesDiscussionDetailResponse>()!;
            response.DiscussionId = entity.Id;
            response.InteractionCount = interactionCount;
            response.AwardCount = awardCount;
            return response;
        }
    }
}
