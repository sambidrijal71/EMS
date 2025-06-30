using EmployeeManagementSystem.Application.DTOs;

namespace EmployeeManagementSystem.Application.Authentication
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(AuthRequest request);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto refreshToken);
    }
}