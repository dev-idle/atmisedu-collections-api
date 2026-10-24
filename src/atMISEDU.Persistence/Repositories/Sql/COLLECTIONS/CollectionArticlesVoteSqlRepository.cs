using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesVoteSqlRepository
    /// </summary>
    public class CollectionArticlesVoteSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionArticlesVote, int>(context), ICollectionArticlesVoteSqlRepository
    {
    }
}