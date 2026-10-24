using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Request for Create <see cref="CollectionArticlesVote"/>
    /// </summary>
    public class CreateCollectionArticlesVoteRequest : ICommand
    {
        public int? ArticleId { get; set; }
        public int? CreatedBy { get; set; }
        public int? VotesPoint { get; set; }
        public int? IsActived { get; set; }
    }
}
