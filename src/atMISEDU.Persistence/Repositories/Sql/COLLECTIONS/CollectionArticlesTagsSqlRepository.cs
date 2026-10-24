using System.Linq.Expressions;
using atMISEDU.Domain.Abstractions.Repositories.Sql.COLLECTIONS;
using atMISEDU.Domain.Entities.COLLECTIONS;
using Microsoft.EntityFrameworkCore;

namespace atMISEDU.Persistence.Repositories.Sql.COLLECTIONS
{
    /// <summary>
    /// Implementation of ICollectionArticlesTagsSqlRepository
    /// </summary>
    public class CollectionArticlesTagsSqlRepository(ApplicationDbContext context) : ICollectionArticlesTagsSqlRepository
    {
        /// <summary>
        /// Find all article tag links that satisfied predicate expression
        /// </summary>
        /// <param name="predicate">Predicate expression</param>
        /// <returns>IQueryable of links that match predicate expression</returns>
        public IQueryable<CollectionArticlesTags> FindAll(Expression<Func<CollectionArticlesTags, bool>>? predicate = null)
        {
            IQueryable<CollectionArticlesTags> query = context.Set<CollectionArticlesTags>().AsNoTracking();
            return predicate != null ? query.Where(predicate) : query;
        }

        /// <summary>
        /// Marked links as Added state
        /// </summary>
        /// <param name="entities">Added links</param>
        public void AddRange(IEnumerable<CollectionArticlesTags> entities)
        {
            context.Set<CollectionArticlesTags>().AddRange(entities);
        }

        /// <summary>
        /// Marked links as Deleted state
        /// </summary>
        /// <param name="entities">Removed links</param>
        public void RemoveMultiple(IEnumerable<CollectionArticlesTags> entities)
        {
            context.Set<CollectionArticlesTags>().RemoveRange(entities);
        }
    }
}
