using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.COLLECTIONS
{
    public class CollectionTags : AggregateRoot<int>
    {
        public string TagsName { get; set; }
        public string? TagsDescription { get; set; }
        public string? TagsIcon { get; set; }
        public string? TagsStyle { get; set; }
        public int CreatedBy { get; set; }
        public int CreatedDate { get; set; }
        public int IsActived { get; set; }
    }
}