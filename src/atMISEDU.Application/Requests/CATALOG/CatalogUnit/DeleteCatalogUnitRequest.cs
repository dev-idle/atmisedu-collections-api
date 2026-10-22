using atMISEDU.Contract.Abstractions;

namespace atMISEDU.Application.Requests.CATALOG.CatalogUnit
{
    /// <summary>
    /// Request for Delete <see cref="CatalogUnit"/>
    /// </summary>
    public class DeleteCatalogUnitRequest : ICommand
    {
        public int? Id { get; set; }
    }
}
