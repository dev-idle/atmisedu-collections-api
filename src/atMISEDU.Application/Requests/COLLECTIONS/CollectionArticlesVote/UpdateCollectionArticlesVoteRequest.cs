using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Request for Update <see cref="CollectionArticlesVote"/>
    /// </summary>
    public class UpdateCollectionArticlesVoteRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? VotesPoint { get; set; }
        public int? IsActived { get; set; }
    }
}
