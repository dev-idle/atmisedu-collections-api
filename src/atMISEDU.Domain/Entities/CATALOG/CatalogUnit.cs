using atMISEDU.Domain.Abstractions.Aggregates;

namespace atMISEDU.Domain.Entities.CATALOG
{
    public class CatalogUnit : AggregateRoot<int>
    {
        public int CugID { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string UnitEnglish { get; set; }
    }
}
