using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Provide repository for <see cref="CollectionArticlesDiscussionInteraction"/>, inherit from <see cref="IGenericSqlRepository{TEntity,TKey}"/>
    /// </summary>
    public interface ICollectionArticlesDiscussionInteractionSqlRepository : IGenericSqlRepository<CollectionArticlesDiscussionInteraction, int>
    {
    }
}