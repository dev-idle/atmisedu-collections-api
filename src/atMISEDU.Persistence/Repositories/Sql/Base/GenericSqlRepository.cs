using atMISEDU.Contract.Shared;
using atMISEDU.Domain.Abstractions.Entities;
using atMISEDU.Domain.Abstractions.Repositories.Sql.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace atMISEDU.Persistence.Repositories.Sql.Base
{
    /// <summary>
    /// Implementation of IGenericRepository
    /// </summary>
    /// <typeparam name="TEntity">Generic type of Domain entity</typeparam>
    /// <typeparam name="TKey">Generic key of Domain entity</typeparam>
    public class GenericSqlRepository<TEntity, TKey> : IGenericSqlRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        /// <summary>
        /// Database context to interact with database
        /// </summary>
        protected readonly ApplicationDbContext context;

        /// <summary>
        /// Entities, as table in database
        /// </summary>
        private DbSet<TEntity>? entities;

        /// <summary>
        /// Constructor of <see cref="GenericSqlRepository{TEntity,TKey}"/>,  inject needed dependency
        /// </summary>
        /// <param name="context"></param>
        public GenericSqlRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Get entity DbSet, if entities is null, use context to get db set of entities
        /// </summary>
        protected DbSet<TEntity> Entities => entities ??= context.Set<TEntity>();

        /// <summary>
        /// Find entity by id. Returned entity can be tracking
        /// </summary>
        /// <param name="id">ID of Domain entity</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity with given id or null if entity with given id not found</returns>
        public Task<TEntity?> FindByIdAsync(TKey id, bool isTracking = false, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = BuildQuery(x => x.Id!.Equals(id), isTracking, includeProperties);
            return query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Find single entity that satisfied predicate expression. Can be tracking
        /// </summary>
        /// <param name="predicate">Predicate expression</param>
        /// <param name="cancellationToken"></param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Domain entity matched expression or null if entity not found</returns>
        public Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate,
                                              bool isTracking = false,
                                              CancellationToken cancellationToken = default,
                                              params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, isTracking, includeProperties);
            return query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Check entity with specific predicate is exist in database
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if entity exist, otherwise false</returns>
        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var query = BuildQuery(predicate);
            return query.AnyAsync(cancellationToken);
        }

        /// <summary>
        /// Find all entity that satisfied predicate expression. Can be tracking
        /// </summary>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="predicate">Predicate expression</param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns><see cref="IQueryable{T}"/> of entities that match predicate expression</returns>
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, bool isTracking = false, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return BuildQuery(predicate, isTracking, includeProperties);
        }

        /// <summary>
        /// Finds all entities with pagination that match the specified predicate.
        /// </summary>
        /// <param name="paginationQuery">Object containing pagination and sorting information.</param>
        /// <param name="predicate">Optional condition to filter the entities.</param>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>A collection of matching entities with pagination applied.</returns>
        public async Task<PagedList<TEntity>> FindAllWithPagingAsync(PaginationQuery paginationQuery,
                                                                     Expression<Func<TEntity, bool>>? predicate = null,
                                                                     bool isTracking = false,
                                                                     CancellationToken cancellationToken = default,
                                                                     params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, isTracking, includeProperties);

            // Count items in database
            int totalItem = await query.CountAsync(cancellationToken);

            // Execute query to get data as items
            List<TEntity> items = await query.Skip((paginationQuery.PageNumber - 1) * paginationQuery.PageSize).Take(paginationQuery.PageSize).ToListAsync(cancellationToken);

            // Return pagination result
            return new PagedList<TEntity>(items, totalItem, paginationQuery.PageNumber, paginationQuery.PageSize);
        }

        /// <summary>
        /// Finds all entities with pagination that match the specified predicate.
        /// </summary>
        /// <param name="skipTakeQuery">Object containing skip, take information.</param>
        /// <param name="predicate">Optional condition to filter the entities.</param>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>A collection of matching entities with pagination applied.</returns>
        public async Task<IEnumerable<TEntity>> FindAllWithSkipTakeAsync(SkipTakeQuery skipTakeQuery,
                                                                         Expression<Func<TEntity, bool>>? predicate = null,
                                                                         bool isTracking = false,
                                                                         CancellationToken cancellationToken = default,
                                                                         params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = BuildQuery(predicate, isTracking, includeProperties);

            // Apply skip take logic
            query = query.Skip(skipTakeQuery.Skip ?? 0);
            if (skipTakeQuery.Take.HasValue) query = query.Take((int)skipTakeQuery.Take);

            // Return pagination result
            return await query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Build Query
        /// </summary>
        /// <param name="predicate">Optional condition to filter the entities.</param>
        /// <param name="isTracking">Tracking state of entity</param>
        /// <param name="includeProperties">Include any relationship if needed</param>
        /// <returns>Query</returns>
        private IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>>? predicate = null,
                                               bool isTracking = false,
                                               params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Initialize query from the entity set
            IQueryable<TEntity> query = Entities.AsQueryable();

            // Include specified properties
            if (includeProperties.Any()) query = IncludeMultiple(query, includeProperties);

            // Apply tracking option
            query = isTracking ? query : query.AsNoTracking();

            // Apply predicate if provided, otherwise return the query
            if (predicate != null) query = query.Where(predicate);

            return query;
        }

        /// <summary>
        /// Marked entity as Added state
        /// </summary>
        /// <param name="entity">Added entity</param>
        public void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Marked entity as Add Range state
        /// </summary>
        /// <param name="entity">Add Range entity</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// Marked entity as Updated state
        /// </summary>
        /// <param name="entity">Updated entity</param>
        public void Update(TEntity entity)
        {
            Entities.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Marked entity as Updated state
        /// </summary>
        /// <param name="entity">Updated entity</param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// Marked entity as Deleted state
        /// </summary>
        /// <param name="entity">Removed entity</param>
        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        /// <summary>
        /// Marked multiple entities as Deleted state
        /// </summary>
        /// <param name="entitiesToRemove">Removed entities</param>
        public void RemoveMultiple(IEnumerable<TEntity> entitiesToRemove)
        {
            Entities.RemoveRange(entitiesToRemove);
        }

        /// <summary>
        /// Extension method of <see cref="IQueryable{T}"/> for including multiple relationship
        /// </summary>
        /// <typeparam name="TEntity">Type of Domain entity</typeparam>
        /// <param name="source">IQueryable source need to including properties</param>
        /// <param name="includeProperties">Properties to be included</param>
        /// <returns><see cref="IQueryable{T}"/> with included properties</returns>
        private IQueryable<TEntity> IncludeMultiple(IQueryable<TEntity> source, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (includeProperties.Any())
                // Each property will be included into source
                source = includeProperties.Aggregate(source, (current, include) => current.Include(include));
            return source;
        }
    }
}