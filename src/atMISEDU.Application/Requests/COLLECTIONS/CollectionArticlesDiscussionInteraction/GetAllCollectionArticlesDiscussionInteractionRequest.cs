using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionArticlesDiscussionInteraction"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionArticlesDiscussionInteractionRequest : IQuery<List<CollectionArticlesDiscussionInteractionResponse>>
    {
        public int? DiscussionId { get; set; }
        public int? ArticleId { get; set; }
        public int? IsActived { get; set; }
    }
}
