using atMISEDU.Contract.Abstractions;
using Entity = atMISEDU.Domain.Entities.CATALOG;

namespace atMISEDU.Application.Requests.CATALOG.CatalogUnit
{
    /// <summary>
    /// Request for GetDetail <see cref="Entity.CatalogUnit"/>
    /// </summary>
    public class GetDetailCatalogUnitRequest : IQuery<Entity.CatalogUnit>
    {
        public int? Id {get; set;}
    }
}
