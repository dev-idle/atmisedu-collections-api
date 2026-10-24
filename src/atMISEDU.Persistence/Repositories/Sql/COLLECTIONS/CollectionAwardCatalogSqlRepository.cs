using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionAwardCatalogSqlRepository
    /// </summary>
    public class CollectionAwardCatalogSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionAwardCatalog, int>(context), ICollectionAwardCatalogSqlRepository
    {
    }
}