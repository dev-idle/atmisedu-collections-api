using System.Reflection;
using atMISEDU.Domain.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace atMISEDU.Persistence
{
    /// <summary>
    /// Application database context 
    /// </summary>
    /// <param name="options">Options for database context</param>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<bool> CheckExistsAllAsyncV2<TEntity, TKey>(IEnumerable<TKey> ids) where TEntity : Entity<TKey>
        {
            var existingIds = await Set<TEntity>().Where(e => ids.Contains(e.Id)).Select(e => e.Id).CountAsync();

            return ids.Count() == existingIds;
        }

        public Task<bool> CheckExistsAsyncV2<TEntity, TKey>(TKey id) where TEntity : Entity<TKey>
        {
            return Set<TEntity>().AnyAsync(e => e.Id!.Equals(id));
        }

        public async Task<bool> CheckExistsAsync<TEntity, TKey>(TKey id) where TEntity : Entity<TKey>
        {
            return await ExistsQueryCache<TEntity, TKey>.Compiled(this, id);
        }

        public async Task<bool> CheckExistsAllAsync<TEntity, TKey>(IEnumerable<TKey> ids) where TEntity : Entity<TKey>
        {
            var existsQuery = ExistsQueryCache<TEntity, TKey>.CompiledAll(this, ids);

            return await existsQuery == ids!.Count();
        }

        // New method: Execute stored procedure and map to a generic type T
        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedureName, params object[] parameters)
        {
            var sql = BuildStoredProcedureSql(storedProcedureName, parameters);
            return await Database.SqlQueryRaw<T>(sql, parameters).ToListAsync();
        }

        // New method: Execute non-query stored procedure
        public async Task<int> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, params object[] parameters)
        {
            var sql = BuildStoredProcedureSql(storedProcedureName, parameters);
            return await Database.ExecuteSqlRawAsync(sql, parameters);
        }

        // Helper method to build SQL for stored procedure
        private string BuildStoredProcedureSql(string storedProcedureName, object[] parameters)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storedProcedureName));

            var parameterPlaceholders = parameters.Length > 0
                ? string.Join(", ", Enumerable.Range(0, parameters.Length).Select(i => $"@p{i}"))
                : string.Empty;

            return $"EXEC {storedProcedureName} {parameterPlaceholders}";
        }

        private static class ExistsQueryCache<TEntity, TKey> where TEntity : Entity<TKey>
        {
            public static readonly Func<ApplicationDbContext, TKey, Task<bool>> Compiled =
                EF.CompileAsyncQuery((ApplicationDbContext db, TKey id) => db.Set<TEntity>().Any(e => e.Id!.Equals(id)));

            public static readonly Func<ApplicationDbContext, IEnumerable<TKey>, Task<int>> CompiledAll =
                EF.CompileAsyncQuery((ApplicationDbContext db, IEnumerable<TKey> ids) => db.Set<TEntity>().Count(e => ids.Contains(e.Id)));
        }
    }
}