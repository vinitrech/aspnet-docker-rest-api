using ASPNETDockerRestAPI.Hypermedia.Abstract;

namespace ASPNETDockerRestAPI.Dtos
{
    public class PagedSearchDto<T> where T : ISupportsHypermedia
    {
        public PagedSearchDto()
        {
        }

        public PagedSearchDto(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }

        public PagedSearchDto(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters) : this(currentPage, pageSize, sortFields, sortDirections)
        {
            Filters = filters;
        }

        public PagedSearchDto(int currentPage, string sortFields, string sortDirections) : this(currentPage, 10, sortFields, sortDirections)
        {
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string? SortFields { get; set; }
        public string? SortDirections { get; set; }
        public Dictionary<string, object>? Filters { get; set; }
        public List<T>? Items { get; set; }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}
