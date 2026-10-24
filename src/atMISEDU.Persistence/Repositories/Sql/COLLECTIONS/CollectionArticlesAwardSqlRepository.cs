using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesAwardSqlRepository
    /// </summary>
    public class CollectionArticlesAwardSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionArticlesAward, int>(context), ICollectionArticlesAwardSqlRepository
    {
    }
}