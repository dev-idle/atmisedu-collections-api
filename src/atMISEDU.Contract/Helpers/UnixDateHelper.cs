using System.Linq.Expressions;
using atMISEDU.Contract.Constants;
using atMISEDU.Contract.Enumerations;

namespace atMISEDU.Contract.Helpers
{
    public static class UnixDateHelper
    {
        public static IQueryable<T> WhereDateLessThanOrEqual<T>(this IQueryable<T> query, Expression<Func<T, int>> dateSelector, int currentTimestamp)
        {
            int currentDay = (currentTimestamp + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;

            return query.Where(Expression.Lambda<Func<T, bool>>(
                Expression.LessThanOrEqual(
                    Expression.Divide(Expression.Add(dateSelector.Body, Expression.Constant(Const.TIME_ZONE_UTC_7, typeof(int))), Expression.Constant(Const.SECONDS_IN_ADAY, typeof(int))),
                    Expression.Constant(currentDay, typeof(int))), dateSelector.Parameters));
        }

        /// <summary>
        /// Filters records based on a comparison of the Unix timestamp column's date with the current date.
        /// </summary>
        /// <typeparam name="T">Type of the table/entity</typeparam>
        /// <param name="query">LINQ query</param>
        /// <param name="dateSelector">Expression to select the timestamp column</param>
        /// <param name="currentTimestamp">Current Unix timestamp</param>
        /// <param name="comparisonType">Type of date comparison</param>
        /// <returns>Filtered LINQ query</returns>
        public static IQueryable<T> WhereDateComparison<T>(this IQueryable<T> query, Expression<Func<T, int>> dateSelector, int currentTimestamp, DateComparisonType comparisonType)
        {
            int currentDay = (currentTimestamp + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;

            Expression comparison;

            // Build the expression: (selected_column + TIME_ZONE_UTC_7) / SecondsInADay
            var dayExpression = Expression.Divide(
                Expression.Add(dateSelector.Body, Expression.Constant(Const.TIME_ZONE_UTC_7, typeof(int))),
                Expression.Constant(Const.SECONDS_IN_ADAY, typeof(int)));

            var currentDayExpression = Expression.Constant(currentDay, typeof(int));

            // Select comparison type
            comparison = comparisonType switch
            {
                DateComparisonType.Equal => Expression.Equal(dayExpression, currentDayExpression),
                DateComparisonType.LessThan => Expression.LessThan(dayExpression, currentDayExpression),
                DateComparisonType.GreaterThan => Expression.GreaterThan(dayExpression, currentDayExpression),
                DateComparisonType.LessThanOrEqual => Expression.LessThanOrEqual(dayExpression, currentDayExpression),
                DateComparisonType.GreaterThanOrEqual => Expression.GreaterThanOrEqual(dayExpression, currentDayExpression),
                _ => throw new ArgumentException("Invalid comparison type")
            };

            // Apply the comparison expression to the query
            return query.Where(Expression.Lambda<Func<T, bool>>(comparison, dateSelector.Parameters));
        }

        /// <summary>
        /// Filters records where the Unix timestamp column's date is between two dates (inclusive).
        /// </summary>
        /// <typeparam name="T">Type of the table/entity</typeparam>
        /// <param name="query">LINQ query</param>
        /// <param name="dateSelector">Expression to select the timestamp column</param>
        /// <param name="startTimestamp">Start Unix timestamp</param>
        /// <param name="endTimestamp">End Unix timestamp</param>
        /// <returns>Filtered LINQ query</returns>
        public static IQueryable<T> WhereDateBetween<T>(this IQueryable<T> query, Expression<Func<T, int>> dateSelector, int startTimestamp, int endTimestamp)
        {
            // Calculate start and end days in GMT+7
            int startDay = (startTimestamp + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;
            int endDay = (endTimestamp + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;

            // Build the expression: (selected_column + TIME_ZONE_UTC_7) / SecondsInADay
            var dayExpression = Expression.Divide(
                Expression.Add(dateSelector.Body, Expression.Constant(Const.TIME_ZONE_UTC_7, typeof(int))),
                Expression.Constant(Const.SECONDS_IN_ADAY, typeof(int)));

            // Filter records where the date is between startDay and endDay
            return query.Where(Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    Expression.GreaterThanOrEqual(dayExpression, Expression.Constant(startDay, typeof(int))),
                    Expression.LessThanOrEqual(dayExpression, Expression.Constant(endDay, typeof(int)))),
                dateSelector.Parameters));
        }
    }
}