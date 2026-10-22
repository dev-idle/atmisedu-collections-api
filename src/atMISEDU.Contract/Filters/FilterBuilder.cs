using System.Linq.Expressions;

namespace atMISEDU.Contract.Filters
{
    /// <summary>
    /// A flexible filter builder for dynamically constructing LINQ expressions
    /// to be used in querying data of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity being filtered.</typeparam>
    public class FilterBuilder<T> where T : class
    {
        private readonly ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        private Expression predicate = Expression.Constant(true);
        private bool hasNullCondition;

        /// <summary>
        /// Gets a value indicating whether any condition added was null or invalid.
        /// Useful for skipping or logging faulty filters.
        /// </summary>
        public bool HasNullCondition => hasNullCondition;

        private static Expression ReplaceParameter(Expression expression, ParameterExpression newParameter, ParameterExpression oldParameter)
        {
            return new ReplaceParameterVisitor(oldParameter, newParameter).Visit(expression);
        }

        // <summary>
        /// Adds an equality filter to the expression based on the provided property and value.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertySelector">The expression selecting the property.</param>
        /// <param name="value">The value to compare to.</param>
        /// <returns>The current <see cref="FilterBuilder{T}"/> instance.</returns>
        public FilterBuilder<T> AddEqualFilter<TProperty>(Expression<Func<T, TProperty>> propertySelector, TProperty value)
        {
            if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
            {
                hasNullCondition = true;
                return this;
            }

            var propertyExpr = ReplaceParameter(propertySelector.Body, parameter, propertySelector.Parameters[0]);

            Expression comparisonExpr;
            if (Nullable.GetUnderlyingType(propertyExpr.Type) != null)
            {
                var hasValue = Expression.Property(propertyExpr, "HasValue");
                var valueExpr = Expression.Property(propertyExpr, "Value");
                comparisonExpr = Expression.AndAlso(hasValue, Expression.Equal(valueExpr, Expression.Constant(value, valueExpr.Type)));
            }
            else
                comparisonExpr = Expression.Equal(propertyExpr, Expression.Constant(value, propertyExpr.Type));

            predicate = Expression.AndAlso(predicate, comparisonExpr);
            return this;
        }

        //public FilterBuilder<T> AddNonAdminNotEqualFilter<TProperty>(IHttpContextAccessor contextAccessor, Expression<Func<T, TProperty>> propertySelector, TProperty valueCheck, int? isActivedFilter)
        //{
        //    if (isActivedFilter != null)
        //    {
        //        return this;
        //    }

        //    bool? isAdmin = contextAccessor.CurrentUserCheckAdmin();
        //    if (isAdmin == null || !isAdmin.Value)
        //    {
        //        // Extract the property expression and replace parameter
        //        var propertyExpr = ReplaceParameter(propertySelector.Body, parameter, propertySelector.Parameters[0]);

        //        // Build the comparison expression: property != value
        //        Expression comparisonExpr;
        //        if (Nullable.GetUnderlyingType(propertyExpr.Type) != null)
        //        {
        //            // Handle nullable types (e.g., x.Property.HasValue && x.Property.Value != value)
        //            var hasValue = Expression.Property(propertyExpr, "HasValue");
        //            var valueExpr = Expression.Property(propertyExpr, "Value");
        //            comparisonExpr = Expression.AndAlso(hasValue, Expression.NotEqual(valueExpr, Expression.Constant(valueCheck, valueExpr.Type)));
        //        }
        //        else
        //        {
        //            // Handle non-nullable types
        //            comparisonExpr = Expression.NotEqual(propertyExpr, Expression.Constant(valueCheck, propertyExpr.Type));
        //        }

        //        // Combine with existing predicate
        //        predicate = Expression.AndAlso(predicate, comparisonExpr);
        //    }

        //    return this;
        //}

        /// <summary>
        /// Adds a string containment filter (i.e. SQL LIKE) for a specific property.
        /// </summary>
        /// <param name="propertySelector">The string property to check.</param>
        /// <param name="value">The value to search for within the string.</param>
        /// <returns>The current <see cref="FilterBuilder{T}"/> instance.</returns>
        public FilterBuilder<T> AddContainFilter(Expression<Func<T, string>> propertySelector, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                hasNullCondition = true;
                return this;
            }

            var propertyExpr = ReplaceParameter(propertySelector.Body, parameter, propertySelector.Parameters[0]);
            var notNull = Expression.NotEqual(propertyExpr, Expression.Constant(null, typeof(string)));
            var contains = Expression.Call(propertyExpr, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(value));
            var filterExpr = Expression.AndAlso(notNull, contains);

            predicate = Expression.AndAlso(predicate, filterExpr);
            return this;
        }

        /// <summary>
        /// Adds an "IN" filter that checks whether the property exists within the given values.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="propertySelector">The expression selecting the property.</param>
        /// <param name="values">The set of values to check against.</param>
        /// <returns>The current <see cref="FilterBuilder{T}"/> instance.</returns>
        public FilterBuilder<T> AddInFilter<TProperty>(Expression<Func<T, TProperty>> propertySelector, IEnumerable<TProperty> values)
        {
            if (!values.Any())
            {
                hasNullCondition = true;
                return this;
            }

            var propertyExpr = ReplaceParameter(propertySelector.Body, parameter, propertySelector.Parameters[0]);
            var constant = Expression.Constant(values);
            var method = typeof(Enumerable).GetMethods()
                .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(TProperty));

            var containsExpr = Expression.Call(method, constant, propertyExpr);
            predicate = Expression.AndAlso(predicate, containsExpr);
            return this;
        }

        /// <summary>
        /// Adds a range filter (between start and end) for a property that implements IComparable.
        /// </summary>
        /// <typeparam name="TProperty">The value type of the property.</typeparam>
        /// <param name="propertySelector">The property to filter.</param>
        /// <param name="start">Optional start value (inclusive).</param>
        /// <param name="end">Optional end value (inclusive).</param>
        /// <returns>The current <see cref="FilterBuilder{T}"/> instance.</returns>
        public FilterBuilder<T> AddRangeFilter<TProperty>(Expression<Func<T, TProperty>> propertySelector,
                                                          TProperty? start,
                                                          TProperty? end)
            where TProperty : struct, IComparable<TProperty>
        {
            var propertyExpr = ReplaceParameter(propertySelector.Body, parameter, propertySelector.Parameters[0]);
            Expression filterExpr = null;

            if (start.HasValue && end.HasValue)
            {
                var minExpr = Expression.GreaterThanOrEqual(propertyExpr, Expression.Constant(start.Value));
                var maxExpr = Expression.LessThanOrEqual(propertyExpr, Expression.Constant(end.Value));
                filterExpr = Expression.AndAlso(minExpr, maxExpr);
            }
            else if (start.HasValue) filterExpr = Expression.GreaterThanOrEqual(propertyExpr, Expression.Constant(start.Value));
            else if (end.HasValue) filterExpr = Expression.LessThanOrEqual(propertyExpr, Expression.Constant(end.Value));

            if (filterExpr != null) predicate = Expression.AndAlso(predicate, filterExpr);

            return this;
        }

        /// <summary>
        /// Adds a multi-property OR-based "contains" filter for multiple string properties.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <param name="propertySelectors">An array of string property expressions to search.</param>
        /// <returns>The current <see cref="FilterBuilder{T}"/> instance.</returns>
        public FilterBuilder<T> AddMultiPropertyFilter(string value, params Expression<Func<T, string>>[] propertySelectors)
        {
            if (string.IsNullOrWhiteSpace(value)) return this;

            Expression? multiFilter = null;
            foreach (var selector in propertySelectors)
            {
                var propertyExpr = ReplaceParameter(selector.Body, parameter, selector.Parameters[0]);
                var notNullExpr = Expression.NotEqual(propertyExpr, Expression.Constant(null, typeof(string)));
                var containsExpr = Expression.Call(propertyExpr, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(value));
                var safeContains = Expression.AndAlso(notNullExpr, containsExpr);

                multiFilter = multiFilter == null ? safeContains : Expression.OrElse(multiFilter, safeContains);
            }

            if (multiFilter != null) predicate = Expression.AndAlso(predicate, multiFilter);

            return this;
        }

        /// <summary>
        /// Adds a group of OR-connected conditions, each defined by a separate FilterBuilder configuration.
        /// </summary>
        /// <param name="groupConfigs">Array of actions configuring each individual filter group.</param>
        /// <returns>The current <see cref="FilterBuilder{T}"/> instance.</returns>
        public FilterBuilder<T> AddOrGroupConditions(params Action<FilterBuilder<T>>[] groupConfigs)
        {
            if (!groupConfigs.Any())
                return this;

            var validGroups = new List<Expression>();
            var allGroupsHaveNull = true;

            foreach (var config in groupConfigs)
            {
                var tempBuilder = new FilterBuilder<T>();
                config(tempBuilder);
                if (tempBuilder.HasNullCondition) continue;

                allGroupsHaveNull = false;
                var groupPredicate = tempBuilder.predicate;
                groupPredicate = ReplaceParameter(groupPredicate, parameter, tempBuilder.parameter);
                validGroups.Add(groupPredicate);
            }
            // Nếu có ít nhất một nhóm hợp lệ
            if (allGroupsHaveNull) return this;

            var orFilter = validGroups.Aggregate(Expression.OrElse);
            predicate = Expression.AndAlso(predicate, orFilter);

            return this;
        }

        /// <summary>
        /// Adds a custom filter expression provided by an external function.
        /// </summary>
        /// <param name="customFilter">A function that returns an expression based on a given input string.</param>
        /// <param name="value">The input value used to build the filter expression.</param>
        /// <returns>The current <see cref="FilterBuilder{T}"/> instance.</returns>
        public FilterBuilder<T> AddCustomFilter(Func<string, Expression<Func<T, bool>>> customFilter, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return this;

            var lambda = customFilter(value);
            var customExpr = ReplaceParameter(lambda.Body, parameter, lambda.Parameters[0]);
            predicate = Expression.AndAlso(predicate, customExpr);

            return this;
        }

        /// <summary>
        /// Builds and returns the final LINQ expression representing all added conditions.
        /// </summary>
        /// <returns>The built <see cref="Expression{Func{T, Boolean}}"/> predicate.</returns>
        public Expression<Func<T, bool>> Build()
        {
            return Expression.Lambda<Func<T, bool>>(predicate, parameter);
        }
    }

    /// <summary>
    /// An expression visitor used to replace one parameter with another in an expression tree.
    /// Useful when combining expressions built using different ParameterExpression instances.
    /// </summary>
    public class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression oldParameter;
        private readonly ParameterExpression newParameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceParameterVisitor"/> class.
        /// </summary>
        /// <param name="oldParameter">The parameter to be replaced.</param>
        /// <param name="newParameter">The new parameter to replace with.</param>
        public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            this.oldParameter = oldParameter;
            this.newParameter = newParameter;
        }

        /// <summary>
        /// Replaces the old parameter with the new one when visiting parameter nodes.
        /// </summary>
        /// <param name="node">The parameter node being visited.</param>
        /// <returns>The new or original parameter node.</returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == oldParameter ? newParameter : base.VisitParameter(node);
        }
    }
}