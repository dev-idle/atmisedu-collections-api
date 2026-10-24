using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Request for Update <see cref="CollectionArticlesDiscussionAward"/>
    /// </summary>
    public class UpdateCollectionArticlesDiscussionAwardRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? AwardId { get; set; }
        public int? IsActived { get; set; }
    }
}
