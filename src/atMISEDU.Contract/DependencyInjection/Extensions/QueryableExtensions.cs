using System.Linq.Expressions;
using System.Reflection;

namespace atMISEDU.Contract.DependencyInjection.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortBy) where T : class
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query;

            var fields = sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            IOrderedQueryable<T>? orderedQuery = null;

            foreach (var field in fields)
            {
                var parts = field.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var propertyName = parts[0];
                var descending = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                // Validate property (skip error)
                var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property == null)
                    continue;

                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                string command = descending ? "OrderByDescending" : "OrderBy";

                if (orderedQuery == null)
                {
                    // First: OrderBy or OrderByDescending
                    orderedQuery = (IOrderedQueryable<T>)typeof(Queryable)
                        .GetMethods()
                        .First(m => m.Name == command && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), property.PropertyType)
                        .Invoke(null, new object[] { query, orderByExp })!;
                }
                else
                {
                    // Second: ThenBy or ThenByDescending
                    command = descending ? "ThenByDescending" : "ThenBy";
                    orderedQuery = (IOrderedQueryable<T>)typeof(Queryable)
                        .GetMethods()
                        .First(m => m.Name == command && m.GetParameters().Length == 2)
                        .MakeGenericMethod(typeof(T), property.PropertyType)
                        .Invoke(null, new object[] { orderedQuery, orderByExp })!;
                }
            }

            return orderedQuery ?? query;
        }
    }
}