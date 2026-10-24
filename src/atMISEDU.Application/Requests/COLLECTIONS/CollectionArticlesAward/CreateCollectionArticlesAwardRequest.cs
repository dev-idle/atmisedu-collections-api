using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.COLLECTIONS.CollectionArticlesAward
{
    /// <summary>
    /// Request for Create <see cref="CollectionArticlesAward"/>
    /// </summary>
    public class CreateCollectionArticlesAwardRequest : ICommand
    {
        public int? ArticleId { get; set; }
        public int? AwardId { get; set; }
        public int? CreatedBy { get; set; }
        public int? IsActived { get; set; }
    }
}
