using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Request for Delete <see cref="CollectionArticlesAward"/>
    /// </summary>
    public class DeleteCollectionArticlesAwardRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
