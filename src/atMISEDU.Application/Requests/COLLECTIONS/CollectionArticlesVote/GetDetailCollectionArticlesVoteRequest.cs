using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionArticlesVote"/>
    /// </summary>
    public class GetDetailCollectionArticlesVoteRequest : IQuery<CollectionArticlesVoteResponse>
    {
        public int? Id { get; set; }
    }
}
