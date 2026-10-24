using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussion;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussion
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionArticlesDiscussion"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionRequest : IQuery<CollectionArticlesDiscussionDetailResponse>
    {
        public int? Id { get; set; }
    }
}
