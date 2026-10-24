using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    public class CollectionArticlesDiscussionInteraction : AggregateRoot<int>
    {
        public int DiscussionId { get; set; }
        public int CreatedBy { get; set; }
        public string IntnType { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }
    }
}