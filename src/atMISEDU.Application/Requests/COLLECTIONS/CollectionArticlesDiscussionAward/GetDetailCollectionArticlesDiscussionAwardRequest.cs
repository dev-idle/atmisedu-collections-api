using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesDiscussionAward;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesDiscussionAward
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionArticlesDiscussionAward"/>
    /// </summary>
    public class GetDetailCollectionArticlesDiscussionAwardRequest : IQuery<CollectionArticlesDiscussionAwardResponse>
    {
        public int? Id { get; set; }
    }
}
