using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesDiscussionInteractionSqlRepository
    /// </summary>
    public class CollectionArticlesDiscussionInteractionSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionArticlesDiscussionInteraction, int>(context), ICollectionArticlesDiscussionInteractionSqlRepository
    {
    }
}