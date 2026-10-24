using atMISEDU.Application.Responses.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionAwardCatalog"/>
    /// </summary>
    public class GetDetailCollectionAwardCatalogRequest : IQuery<CollectionAwardCatalogResponse>
    {
        public int? Id { get; set; }
    }
}
