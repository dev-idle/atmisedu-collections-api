using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    public class CollectionArticlesDiscussionAward : AggregateRoot<int>
    {
        public int DiscussionId { get; set; }
        public int AwardId { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }
    }
}