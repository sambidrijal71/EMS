using EmployeeManagementSystem.Domain.Enums;

namespace EmployeeManagementSystem.Application.DTOs
{
    public class UpdateEmployeeDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string DateOfBirth { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required Position Position { get; set; }
        public EmploymentStatus Status { get; set; }
        public required Department Department { get; set; }
        public EmployeeActivityStatus ActivityStatus { get; set; }
        public DateOnly DateHired { get; set; }
        public Decimal Salary { get; set; }
    }
}