using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionArticlesDiscussionInteraction"/>, field names are same as database columns
    /// </summary>
    public class CollectionArticlesDiscussionInteractionResponse
    {
        public int InteractionId { get; set; }
        public int DiscussionId { get; set; }
        public int CreatedBy { get; set; }
        public string IntnType { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }

        /// <summary>
        /// Create response from <see cref="Entities.CollectionArticlesDiscussionInteraction"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionArticlesDiscussionInteractionResponse FromEntity(Entities.CollectionArticlesDiscussionInteraction entity)
        {
            var response = entity.MapTo<CollectionArticlesDiscussionInteractionResponse>()!;
            response.InteractionId = entity.Id;
            return response;
        }
    }
}
