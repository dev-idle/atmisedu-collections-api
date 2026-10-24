using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionArticlesDiscussionAward"/>, field names are same as database columns
    /// </summary>
    public class CollectionArticlesDiscussionAwardResponse
    {
        public int DaId { get; set; }
        public int DiscussionId { get; set; }
        public int AwardId { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }

        /// <summary>
        /// Create response from <see cref="Entities.CollectionArticlesDiscussionAward"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionArticlesDiscussionAwardResponse FromEntity(Entities.CollectionArticlesDiscussionAward entity)
        {
            var response = entity.MapTo<CollectionArticlesDiscussionAwardResponse>()!;
            response.DaId = entity.Id;
            return response;
        }
    }
}
