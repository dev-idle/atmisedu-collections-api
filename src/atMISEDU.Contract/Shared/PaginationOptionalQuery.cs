namespace atMISEDU.Contract.Shared
{
    public class PaginationOptionalQuery : SkipTakeQuery
    {
        public string? SortBy { get; set; }
        public bool? IsDescending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;

        public PaginationOptionalQuery()
        {
        }

        public PaginationOptionalQuery(int pageNumber, int pageSize, int? skip, int? take, string? sortBy = null, bool? isDescending = null)
            : base(skip, take, sortBy)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            IsDescending = isDescending;
            SortBy = sortBy;
        }
    }
}