using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticles
{
    /// <summary>
    /// Request for Delete <see cref="CollectionArticles"/>
    /// </summary>
    public class DeleteCollectionArticlesRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
