using EmployeeManagementSystem.Domain.Enums;

namespace EmployeeManagementSystem.Application.DTOs
{
    public class ReadEmployeeDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Position { get; set; }
        public required string Status { get; set; }
        public required string Department { get; set; }
        public required string ActivityStatus { get; set; }
    }
}