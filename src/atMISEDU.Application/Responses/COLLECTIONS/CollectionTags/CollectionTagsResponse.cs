using atMISEDU.Contract.DependencyInjection.Extensions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Responses.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Response data of <see cref="Entities.CollectionTags"/>, field names are same as database columns
    /// </summary>
    public class CollectionTagsResponse
    {
        public int KeyTags { get; set; }
        public string TagsName { get; set; }
        public string? TagsDescription { get; set; }
        public string? TagsIcon { get; set; }
        public string? TagsStyle { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }

        /// <summary>
        /// Create response from <see cref="Entities.CollectionTags"/>
        /// </summary>
        /// <param name="entity">Entity to map</param>
        /// <returns>Response with same data of entity</returns>
        public static CollectionTagsResponse FromEntity(Entities.CollectionTags entity)
        {
            var response = entity.MapTo<CollectionTagsResponse>()!;
            response.KeyTags = entity.Id;
            return response;
        }
    }
}
