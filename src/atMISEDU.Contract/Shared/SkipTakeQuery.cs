namespace atMISEDU.Contract.Shared
{
    public class SkipTakeQuery
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public string? SortBy { get; set; }

        public SkipTakeQuery()
        {
        }

        public SkipTakeQuery(int? skip, int? take, string? sortBy = null)
        {
            Skip = skip < 0 ? null : skip;
            Take = take < 0 ? null : take;
            SortBy = sortBy;
        }
    }
}