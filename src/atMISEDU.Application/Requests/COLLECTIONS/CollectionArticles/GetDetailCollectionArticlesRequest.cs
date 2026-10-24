using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Request for GetDetail <see cref="Entities.CollectionArticles"/>
    /// </summary>
    public class GetDetailCollectionArticlesRequest : IQuery<CollectionArticlesDetailResponse>
    {
        public int? Id { get; set; }
    }
}
