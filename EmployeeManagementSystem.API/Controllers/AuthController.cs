using System.Security.Claims;
using EmployeeManagementSystem.API.Helper;
using EmployeeManagementSystem.Application.Authentication;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<UserDto>> GetUserByUsername([FromQuery] string username)
        {
            var result = await _userRepository.GetUserByUsernameAsync(username);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthRequest authRequest)
        {
            var isValid = await _userRepository.LoginUserAsync(authRequest.Username, authRequest.Password);
            if (!isValid) return Unauthorized(ErrorTemplates.Unauthorized("Invalid username or password."));
            var authResponse = await _authService.AuthenticateAsync(authRequest);
            return Ok(authResponse);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return Unauthorized();
            var isValid = await _userRepository.LogoutUserAsync(username);
            if (!isValid) return BadRequest(ErrorTemplates.BadRequest("Failed to logout."));
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] CreateUserDto createUserDto)
        {
            var result = await _userRepository.RegisterUserAsync(createUserDto);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("update-user")]
        public async Task<ActionResult> UpdateUserCredentials([FromQuery] string username, [FromQuery] string currentPassword, [FromQuery] string newPassword)
        {
            var result = await _userRepository.UpdateUserPasswordAsync(username, currentPassword, newPassword);
            if (result != null) return NoContent();
            return BadRequest(ErrorTemplates.BadRequest("Cannot update the user details."));
        }

        [Authorize]
        [HttpGet("validate-token")]
        public async Task<ActionResult> ValidateToken()
        {
            var username = User.Identity?.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var result = await Task.FromResult(new { username, role });
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenRequestDto refreshToken)
        {
            var response = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(response);
        }
    }
}