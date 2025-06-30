using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagementSystem.Domain.Enums;
using EmployeeManagementSystem.Domain.ValueObjects;

namespace EmployeeManagementSystem.Domain.Entities
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public required UserName Username { get; set; }
        public required Email Email { get; set; }
        public Password? Password { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}