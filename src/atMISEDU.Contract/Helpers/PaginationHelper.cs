using atMISEDU.Contract.DependencyInjection.Extensions;
using atMISEDU.Contract.Shared;

namespace atMISEDU.Contract.Helpers
{
    public static class PaginationHelper
    {
        public static object ApplyPaginationSkipTake<T>(IQueryable<T> data, PaginationOptionalQuery query) where T : class =>
            query.PageNumber > 0 ? ApplyPaging(data, query.MapTo<PaginationQuery>()!) : ApplySkipTake(data, query.MapTo<SkipTakeQuery>()!);

        public static IQueryable<T> ApplySkipTake<T>(IQueryable<T> data, SkipTakeQuery query) where T : class
        {
            data = data.ApplySorting(query.SortBy); // add sort

            // Apply skip items.
            data = data.Skip(query.Skip ?? 0);

            // Apply take items.
            if (query.Take.HasValue) return data.Take(query.Take.Value);

            return data;
        }

        public static PagedList<T> ApplyPaging<T>(IQueryable<T> data, PaginationQuery query) where T : class
        {
            var totalItems = data.Count();

            data = data.ApplySorting(query.SortBy); // add sort

            //Pagination
            var items = data.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);

            return new PagedList<T>(items, totalItems, query.PageNumber, query.PageSize);
        }
    }
}