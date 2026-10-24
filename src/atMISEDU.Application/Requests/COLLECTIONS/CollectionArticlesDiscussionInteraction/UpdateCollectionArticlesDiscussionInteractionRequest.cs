using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Request for Update <see cref="CollectionArticlesDiscussionInteraction"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionInteractionRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? IntnType { get; set; }
        public int? IsActived { get; set; }
    }
}
