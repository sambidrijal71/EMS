namespace EmployeeManagementSystem.Application.DTOs
{
    public class RefreshTokenRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}