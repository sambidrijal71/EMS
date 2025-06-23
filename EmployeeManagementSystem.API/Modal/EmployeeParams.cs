namespace EmployeeManagementSystem.API.Modal
{
    public class EmployeeParams : PaginationParams
    {
        public string? SortBy { get; set; }
        public string? SearchKey { get; set; }
    }
}