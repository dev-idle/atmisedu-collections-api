using atMISEDU.Application.Responses.COLLECTIONS.CollectionAwardCatalog;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionAwardCatalog"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionAwardCatalogRequest : IQuery<List<CollectionAwardCatalogResponse>>
    {
        public int? IsActived { get; set; }
    }
}
