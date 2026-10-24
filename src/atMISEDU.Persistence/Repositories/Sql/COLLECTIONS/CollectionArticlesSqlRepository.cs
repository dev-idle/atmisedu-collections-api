using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesSqlRepository
    /// </summary>
    public class CollectionArticlesSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionArticles, int>(context), ICollectionArticlesSqlRepository
    {
    }
}