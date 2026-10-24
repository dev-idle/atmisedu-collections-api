using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Request for Delete <see cref="CollectionAwardCatalog"/>
    /// </summary>
    public class DeleteCollectionAwardCatalogRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
