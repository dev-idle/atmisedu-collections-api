using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.HUBS
{
    public class HubsChannel : AggregateRoot<int>
    {
        public string CnName { get; set; }
        public string CnUrl { get; set; }
        public string CnIcon { get; set; }
        public string CnStyle { get; set; }
        public int CnOrder { get; set; }
        public int IsActived { get; set; }
    }
}
