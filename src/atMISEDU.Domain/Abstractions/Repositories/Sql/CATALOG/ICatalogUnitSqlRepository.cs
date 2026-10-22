using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Entities.CATALOG;

namespace atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG
{
    /// <summary>
    /// Provide repository for <see cref="CatalogUnit"/>, inherit from <see cref="IGenericSqlRepository{TEntity,TKey}"/>
    /// </summary>
    public interface ICatalogUnitSqlRepository : IGenericSqlRepository<CatalogUnit, int>
    {
    }
}
