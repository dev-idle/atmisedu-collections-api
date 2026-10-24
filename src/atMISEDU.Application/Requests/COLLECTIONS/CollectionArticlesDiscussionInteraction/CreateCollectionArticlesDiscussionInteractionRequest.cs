using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Request for Create <see cref="CollectionArticlesDiscussionInteraction"/>
    /// </summary>
    public class CreateCollectionArticlesDiscussionInteractionRequest : ICommand
    {
        public int? DiscussionId { get; set; }
        public int? CreatedBy { get; set; }
        public string? IntnType { get; set; }
        public int? IsActived { get; set; }
    }
}
