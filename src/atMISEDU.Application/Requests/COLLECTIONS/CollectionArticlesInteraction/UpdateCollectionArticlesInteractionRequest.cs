using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Request for Update <see cref="CollectionArticlesInteraction"/>
    /// </summary>
    public class UpdateCollectionArticlesInteractionRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? IntnType { get; set; }
        public int? IsActived { get; set; }
    }
}
