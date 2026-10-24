using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionArticlesDiscussionAward"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionArticlesDiscussionAwardRequest : IQuery<List<CollectionArticlesDiscussionAwardResponse>>
    {
        public int? DiscussionId { get; set; }
        public int? IsActived { get; set; }
    }
}
