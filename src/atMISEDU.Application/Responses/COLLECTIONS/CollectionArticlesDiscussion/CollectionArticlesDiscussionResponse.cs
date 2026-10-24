using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionArticlesDiscussion"/>, field names are same as database columns
    /// </summary>
    public class CollectionArticlesDiscussionResponse
    {
        public int DiscussionId { get; set; }
        public int? DiscussionPid { get; set; }
        public int ArticleId { get; set; }
        public int CreatedBy { get; set; }
        public string? DisContent { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }

        /// <summary>
        /// Create response from <see cref="Entities.CollectionArticlesDiscussion"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionArticlesDiscussionResponse FromEntity(Entities.CollectionArticlesDiscussion entity)
        {
            var response = entity.MapTo<CollectionArticlesDiscussionResponse>()!;
            response.DiscussionId = entity.Id;
            return response;
        }
    }
}
