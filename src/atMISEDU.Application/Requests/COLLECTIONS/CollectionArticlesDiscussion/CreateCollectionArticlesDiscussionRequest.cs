using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Request for Create <see cref="CollectionArticlesDiscussion"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionRequest : ICommand
    {
        public int? DiscussionPid { get; set; }
        public int? ArticleId { get; set; }
        public int? CreatedBy { get; set; }
        public string? DisContent { get; set; }
        public int? IsActived { get; set; }
    }
}
