using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Request for Create <see cref="CollectionArticlesDiscussionAward"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionAwardRequest : ICommand
    {
        public int? DiscussionId { get; set; }
        public int? AwardId { get; set; }
        public int? CreatedBy { get; set; }
        public int? IsActived { get; set; }
    }
}
