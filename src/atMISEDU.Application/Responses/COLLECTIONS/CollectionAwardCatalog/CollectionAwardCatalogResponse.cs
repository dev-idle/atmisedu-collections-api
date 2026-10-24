using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionAwardCatalog"/>, field names are same as database columns
    /// </summary>
    public class CollectionAwardCatalogResponse
    {
        public int AwardId { get; set; }
        public string AwIcon { get; set; }
        public int AwPoint { get; set; }
        public int IsActived { get; set; }

        /// <summary>
        /// Create response from <see cref="Entities.CollectionAwardCatalog"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionAwardCatalogResponse FromEntity(Entities.CollectionAwardCatalog entity)
        {
            var response = entity.MapTo<CollectionAwardCatalogResponse>()!;
            response.AwardId = entity.Id;
            return response;
        }
    }
}
