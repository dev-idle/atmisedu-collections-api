using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using atMISEDU.Persistence.Repositories.Sql.Base;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesInteractionSqlRepository
    /// </summary>
    public class CollectionArticlesInteractionSqlRepository(ApplicationDbContext context) : GenericSqlRepository<CollectionArticlesInteraction, int>(context), ICollectionArticlesInteractionSqlRepository
    {
    }
}