using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Request for Delete <see cref="CollectionArticlesDiscussionInteraction"/>
    /// </summary>
    public class DeleteCollectionArticlesDiscussionInteractionRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
