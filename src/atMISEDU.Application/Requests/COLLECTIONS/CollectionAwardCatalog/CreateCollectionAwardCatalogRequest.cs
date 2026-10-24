using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Request for Create <see cref="CollectionAwardCatalog"/>
    /// </summary>
    public class CreateCollectionAwardCatalogRequest : ICommand
    {
        public string? AwIcon { get; set; }
        public int? AwPoint { get; set; }
        public int? IsActived { get; set; }
    }
}
