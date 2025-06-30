using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(User user);
        Task<string> RefreshToken();
    }
}