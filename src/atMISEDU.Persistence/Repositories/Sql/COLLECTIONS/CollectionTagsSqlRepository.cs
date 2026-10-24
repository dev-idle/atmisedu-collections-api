using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionTagsSqlRepository
    /// </summary>
    public class CollectionTagsSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionTags, int>(context), ICollectionTagsSqlRepository
    {
    }
}