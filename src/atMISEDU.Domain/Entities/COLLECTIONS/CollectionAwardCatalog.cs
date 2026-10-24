using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    public class CollectionAwardCatalog : AggregateRoot<int>
    {
        public string AwIcon { get; set; }
        public int AwPoint { get; set; }
        public int IsActived { get; set; }
    }
}