using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EmployeeManagementSystem.Application.Authentication;
using EmployeeManagementSystem.Application.Settings;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagementSystem.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        public async Task<string> GenerateToken(User user)
        {
            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, user.Username.Value),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiry,
                signingCredentials: credentials

            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.WriteToken(token);
            return await Task.FromResult(jwt);
        }
        public async Task<string> RefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return await Task.FromResult(Convert.ToBase64String(randomBytes));
        }
    }
}