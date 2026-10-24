using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionArticlesInteraction"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionArticlesInteractionRequest : IQuery<List<CollectionArticlesInteractionResponse>>
    {
        public int? ArticleId { get; set; }
        public int? IsActived { get; set; }
    }
}
