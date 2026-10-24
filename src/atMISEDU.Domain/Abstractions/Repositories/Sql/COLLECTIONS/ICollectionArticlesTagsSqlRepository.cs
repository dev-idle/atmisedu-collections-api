using System.Linq.Expressions;
using atMISEDU.Domain.Entities.COLLECTIONS;

namespace atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Provide repository for <see cref="CollectionArticlesTags"/>.
    /// Composite key table, so it does not inherit from IGenericSqlRepository
    /// </summary>
    public interface ICollectionArticlesTagsSqlRepository
    {
        /// <summary>
        /// Find all article tag links that satisfied predicate expression
        /// </summary>
        /// <param name="predicate">Predicate expression</param>
        /// <returns>IQueryable of links that match predicate expression</returns>
        IQueryable<CollectionArticlesTags> FindAll(Expression<Func<CollectionArticlesTags, bool>>? predicate = null);

        /// <summary>
        /// Marked links as Added state
        /// </summary>
        /// <param name="entities">Added links</param>
        void AddRange(IEnumerable<CollectionArticlesTags> entities);

        /// <summary>
        /// Marked links as Deleted state
        /// </summary>
        /// <param name="entities">Removed links</param>
        void RemoveMultiple(IEnumerable<CollectionArticlesTags> entities);
    }
}
