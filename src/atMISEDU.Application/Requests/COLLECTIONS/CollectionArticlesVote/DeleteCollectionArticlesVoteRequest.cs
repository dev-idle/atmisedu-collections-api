using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Request for Delete <see cref="CollectionArticlesVote"/>
    /// </summary>
    public class DeleteCollectionArticlesVoteRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
