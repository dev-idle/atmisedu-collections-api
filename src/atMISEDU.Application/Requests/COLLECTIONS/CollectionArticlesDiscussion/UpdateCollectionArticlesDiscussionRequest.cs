using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Request for Update <see cref="CollectionArticlesDiscussion"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? DisContent { get; set; }
        public int? IsActived { get; set; }
    }
}
