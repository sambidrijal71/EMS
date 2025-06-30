namespace EmployeeManagementSystem.Application.Authentication
{
    public class AuthResponse
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthResponse(string username, string role, string accessToken, string refreshToken)
        {
            Username = username;
            Role = role;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}