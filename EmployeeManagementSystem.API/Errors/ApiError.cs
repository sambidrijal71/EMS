namespace EmployeeManagementSystem.API.Errors
{
    public class ApiError
    {
        public string? Type { get; set; }
        public required string Title { get; set; }
        public int Status { get; set; }
        public required String Detail { get; set; } = string.Empty;
        public string? Path { get; set; }
    }
}