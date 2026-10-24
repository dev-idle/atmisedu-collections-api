using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Request for Delete <see cref="CollectionArticlesDiscussionAward"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionAwardRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
