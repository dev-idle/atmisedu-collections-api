using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    public class CollectionArticlesVote : AggregateRoot<int>
    {
        public int ArticleId { get; set; }
        public int CreatedBy { get; set; }
        public int VotesPoint { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }
    }
}