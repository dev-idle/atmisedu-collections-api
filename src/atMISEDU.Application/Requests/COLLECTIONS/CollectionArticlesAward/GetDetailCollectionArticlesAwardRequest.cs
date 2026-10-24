using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionArticlesAward"/>
    /// </summary>
    public class GetDetailCollectionArticlesAwardRequest : IQuery<CollectionArticlesAwardResponse>
    {
        public int? Id { get; set; }
    }
}
