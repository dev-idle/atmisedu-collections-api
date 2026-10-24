using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    public class CollectionArticlesDiscussion : AggregateRoot<int>
    {
        public int? DiscussionPid { get; set; }
        public int ArticleId { get; set; }
        public int CreatedBy { get; set; }
        public string? DisContent { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }
    }
}