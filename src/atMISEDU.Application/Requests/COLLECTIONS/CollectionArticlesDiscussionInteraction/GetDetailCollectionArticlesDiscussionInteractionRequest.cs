using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionInteraction;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionInteraction
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionArticlesDiscussionInteraction"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionInteractionRequest : IQuery<CollectionArticlesDiscussionInteractionResponse>
    {
        public int? Id { get; set; }
    }
}
