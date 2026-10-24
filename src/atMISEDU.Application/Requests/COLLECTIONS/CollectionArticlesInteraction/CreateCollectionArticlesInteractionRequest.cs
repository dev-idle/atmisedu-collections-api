using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Request for Create <see cref="CollectionArticlesInteraction"/>
    /// </summary>
    public class CreateCollectionArticlesInteractionRequest : ICommand
    {
        public int? ArticleId { get; set; }
        public int? CreatedBy { get; set; }
        public string? IntnType { get; set; }
        public int? IsActived { get; set; }
    }
}
