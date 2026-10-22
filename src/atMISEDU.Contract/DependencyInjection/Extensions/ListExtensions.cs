namespace atMISEDU.Contract.DependencyInjection.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<T> GetDuplicates<T>(this IEnumerable<T> list)
        {
            return list.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
        }
    }
}