namespace Data.Helper
{
    public class Paging<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; } = [];
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public Paging(IEnumerable<T> data, int totalCount, int pageIndex, int pageSize)
        {
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }
    }
}
