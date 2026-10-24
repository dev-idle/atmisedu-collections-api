using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionAwardCatalog
{
    /// <summary>
    /// Request for Update <see cref="CollectionAwardCatalog"/>
    /// </summary>
    public class UpdateCollectionAwardCatalogRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? AwIcon { get; set; }
        public int? AwPoint { get; set; }
        public int? IsActived { get; set; }
    }
}
