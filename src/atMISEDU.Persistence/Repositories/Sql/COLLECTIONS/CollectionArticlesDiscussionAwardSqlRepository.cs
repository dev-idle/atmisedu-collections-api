using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesDiscussionAwardSqlRepository
    /// </summary>
    public class CollectionArticlesDiscussionAwardSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionArticlesDiscussionAward, int>(context), ICollectionArticlesDiscussionAwardSqlRepository
    {
    }
}