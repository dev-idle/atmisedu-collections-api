using atMISEDU.Application.Responses.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionTags"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionTagsRequest : IQuery<List<CollectionTagsResponse>>
    {
        public int? IsActived { get; set; }
    }
}
