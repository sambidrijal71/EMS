using EmployeeManagementSystem.Application.Exceptions;
using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Domain.ValueObjects;

namespace EmployeeManagementSystem.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(AppDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Username.Value == username) ?? throw new UserAuthenticationException("User doen not exist.");
            return user;
        }
        public async Task<bool> LoginUserAsync(string username, string password)
        {
            await VerifyUser(username, password);
            return true;
        }

        public async Task<bool> LogoutUserAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Username.Value == username) ?? throw new UserAuthenticationException("User doen not exist.");
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiryTime = DateTime.MinValue;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UserDto> RegisterUserAsync(CreateUserDto createUserDto)
        {
            var existingUser = await _context.Users.AnyAsync(i => i.Username.Value == createUserDto.Username);
            if (existingUser) throw new UserAuthenticationException("User already exists");
            var user = new User
            {
                Username = new UserName(createUserDto.Username),
                Email = new Email(createUserDto.Email),
            };

            var hashedPassword = _passwordHasher.HashPassword(user, createUserDto.Password);
            user.Password = new Password(hashedPassword);
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) throw new UserAuthenticationException("Could not create a new user.");
            return MapToUserDto(user);
        }

        public async Task<UserDto> UpdateUserPasswordAsync(string username, string currentPassword, string newPassword)
        {
            var user = await VerifyUser(username, currentPassword);
            var userPassword = _passwordHasher.VerifyHashedPassword(user, user.Password.Value, newPassword);
            if (userPassword == PasswordVerificationResult.Success) throw new UserAuthenticationException("New password cannot be same as old password.");
            var hashedPassword = _passwordHasher.HashPassword(user, newPassword);
            user.Password = new Password(hashedPassword);
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) throw new UserAuthenticationException("Could not update the user.");
            return MapToUserDto(user);
        }
        private async Task<User> VerifyUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Username.Value == username) ?? throw new UserAuthenticationException("Either Username or Password is invalid.");
            var userPassword = _passwordHasher.VerifyHashedPassword(user, user.Password.Value, password);
            if (userPassword != PasswordVerificationResult.Success) throw new UserAuthenticationException("Either Username or Password is invalid.");
            return user;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private static UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                Username = user.Username.Value,
                Email = user.Email.Value,
                Role = user.Role.ToString()
            };
        }
    }
}