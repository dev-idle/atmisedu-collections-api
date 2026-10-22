using atMISEDU.Contract.Abstractions;
using System.Text.Json.Serialization;

namespace atMISEDU.Application.Requests.CATALOG.CatalogUnit
{
    /// <summary>
    /// Request for Update <see cref="CatalogUnit"/>
    /// </summary>
    public class UpdateCatalogUnitRequest : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public int? CugID { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; }
        public string? UnitEnglish { get; set; }
    }
}
