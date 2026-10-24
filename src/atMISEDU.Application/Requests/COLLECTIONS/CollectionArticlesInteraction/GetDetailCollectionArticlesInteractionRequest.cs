using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesInteraction;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesInteraction
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionArticlesInteraction"/>
    /// </summary>
    public class GetDetailCollectionArticlesInteractionRequest : IQuery<CollectionArticlesInteractionResponse>
    {
        public int? Id { get; set; }
    }
}
