using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Request for Delete <see cref="CollectionArticlesDiscussion"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
