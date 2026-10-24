using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesVote;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesVote
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionArticlesVote"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionArticlesVoteRequest : IQuery<List<CollectionArticlesVoteResponse>>
    {
        public int? ArticleId { get; set; }
        public int? IsActived { get; set; }
    }
}
