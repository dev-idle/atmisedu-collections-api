using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionArticlesInteraction"/>, field names are same as database columns
    /// </summary>
    public class CollectionArticlesInteractionResponse
    {
        public int InteractionId { get; set; }
        public int ArticleId { get; set; }
        public int CreatedBy { get; set; }
        public string IntnType { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }

        /// <summary>
        /// Create response from <see cref="Entities.CollectionArticlesInteraction"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionArticlesInteractionResponse FromEntity(Entities.CollectionArticlesInteraction entity)
        {
            var response = entity.MapTo<CollectionArticlesInteractionResponse>()!;
            response.InteractionId = entity.Id;
            return response;
        }
    }
}
