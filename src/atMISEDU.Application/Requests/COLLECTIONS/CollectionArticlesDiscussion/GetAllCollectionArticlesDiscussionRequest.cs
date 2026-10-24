using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionArticlesDiscussion"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionArticlesDiscussionRequest : IQuery<List<CollectionArticlesDiscussionResponse>>
    {
        public int? ArticleId { get; set; }
        public int? DiscussionPid { get; set; }
        public int? IsActived { get; set; }
    }
}
