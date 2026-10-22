using atMISEDU.Contract.Abstractions;
using Entity = atMISEDU.Domain.Entities.CATALOG;

namespace atMISEDU.Application.Requests.CATALOG.CatalogUnit
{
    /// <summary>
    /// Request for GetAll <see cref="Entity.CatalogUnit"/>
    /// </summary>
    public class GetAllCatalogUnitRequest : IQuery<IQueryable<Entity.CatalogUnit>>
    {
    }
}
