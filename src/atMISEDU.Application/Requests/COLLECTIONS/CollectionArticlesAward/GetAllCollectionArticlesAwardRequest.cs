using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticlesAward;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionArticlesAward"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionArticlesAwardRequest : IQuery<List<CollectionArticlesAwardResponse>>
    {
        public int? ArticleId { get; set; }
        public int? IsActived { get; set; }
    }
}
