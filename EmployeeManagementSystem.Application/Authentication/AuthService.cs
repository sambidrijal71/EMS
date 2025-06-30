using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Exceptions;
using EmployeeManagementSystem.Application.Interfaces;

namespace EmployeeManagementSystem.Application.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResponse> AuthenticateAsync(AuthRequest request)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(request.Username) ?? throw new UserAuthenticationException($"Username {request.Username} not found");
                var userPassword = await _userRepository.LoginUserAsync(request.Username, request.Password);
                if (!userPassword) throw new UserAuthenticationException("Either Username or Password is Invalid. Please try again.");
                var token = await _jwtTokenGenerator.GenerateToken(user);
                var refreshToken = await _jwtTokenGenerator.RefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _userRepository.SaveChangesAsync();
                var response = new AuthResponse(
                    user.Username.Value,
                    user.Role.ToString(),
                    token,
                    refreshToken
                );
                return response;
            }
            catch (Exception ex)
            {

                throw new UserAuthenticationException("An unexpected error occured during authentication.", ex);
            }
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto refreshToken)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(refreshToken.Username) ?? throw new UserAuthenticationException($"Username {refreshToken.Username} not found");
                if (user.RefreshToken != refreshToken.RefreshToken)
                    throw new UserAuthenticationException($"Refresh token mismatch. Expected: {user.RefreshToken}, Provided: {refreshToken.RefreshToken}");

                if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    throw new UserAuthenticationException("Refresh token expired.");
                var newAccessToken = await _jwtTokenGenerator.GenerateToken(user);
                var newRefreshToken = await _jwtTokenGenerator.RefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _userRepository.SaveChangesAsync();
                return new AuthResponse(
            user.Username.Value,
            user.Role.ToString(),
            newAccessToken,
            newRefreshToken
        );
            }
            catch (Exception ex)
            {

                throw new UserAuthenticationException("An unexpected error occurred during token refresh.", ex);
            }
        }
    }
}