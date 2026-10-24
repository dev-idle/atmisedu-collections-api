using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Request for Delete <see cref="CollectionArticlesInteraction"/>
    /// </summary>
    public class DeleteCollectionArticlesInteractionRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
