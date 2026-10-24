using atMISEDU.Application.Responses.COLLECTIONS.CollectionArticles;
using atMISEDU.Contract.Abstractions;
using Entities = atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Request for GetAll <see cref="Entities.CollectionArticles"/>, filters are sent by query string
    /// </summary>
    public class GetAllCollectionArticlesRequest : IQuery<List<CollectionArticlesResponse>>
    {
        public int? IsActived { get; set; }
        // Get articles which have link with this tag
        public int? KeyTags { get; set; }
    }
}
