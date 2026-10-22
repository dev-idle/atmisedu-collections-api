using atMISEDU.Domain.Entities.CATALOG;
using atMISEDU.Domain.Abstractions.Repositories.Sql.CATALOG;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.CATALOG
{
    /// <summary>
    /// Implementation of ICatalogUnitSqlRepository
    /// </summary>
    public class CatalogUnitSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CatalogUnit, int>(context), ICatalogUnitSqlRepository
    {
    }
}
