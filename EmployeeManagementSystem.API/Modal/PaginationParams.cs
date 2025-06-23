namespace EmployeeManagementSystem.API.Modal
{
    public class PaginationParams
    {
        public const int MaxPageSize = 50;
        private int _pageSize = 6;
        public int PageSize { get => _pageSize; set => _pageSize = value > MaxPageSize ? MaxPageSize : value; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
    }
}