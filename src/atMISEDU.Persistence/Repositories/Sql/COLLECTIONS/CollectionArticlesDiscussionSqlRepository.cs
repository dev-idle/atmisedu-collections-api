using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesDiscussionSqlRepository
    /// </summary>
    public class CollectionArticlesDiscussionSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionArticlesDiscussion, int>(context), ICollectionArticlesDiscussionSqlRepository
    {
    }
}