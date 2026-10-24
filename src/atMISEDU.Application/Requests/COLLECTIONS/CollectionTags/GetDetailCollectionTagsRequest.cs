using atMISEDU.Application.Responses.COLLECTIONS.CollectionTags;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionTags
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionTags"/>
    /// </summary>
    public class GetDetailCollectionTagsRequest : IQuery<CollectionTagsResponse>
    {
        public int? Id { get; set; }
    }
}
