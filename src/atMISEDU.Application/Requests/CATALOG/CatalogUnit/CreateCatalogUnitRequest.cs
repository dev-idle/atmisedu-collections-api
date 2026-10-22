using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.CATALOG.CatalogUnit
{
    /// <summary>
    /// Request for Create <see cref="CatalogUnit"/>
    /// </summary>
    public class CreateCatalogUnitRequest : ICommand
    {
        public int? CugID { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; }
        public string? UnitEnglish { get; set; }
    }
}
