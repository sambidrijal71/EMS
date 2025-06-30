using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> LoginUserAsync(string username, string password);
        Task<bool> LogoutUserAsync(string username);
        Task<UserDto> RegisterUserAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateUserPasswordAsync(string username, string currentPassword, string newPassword);
        Task<bool> SaveChangesAsync();

    }
}