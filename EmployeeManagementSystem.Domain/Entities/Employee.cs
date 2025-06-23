using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagementSystem.Domain.Enums;
using EmployeeManagementSystem.Domain.ValueObjects;

namespace EmployeeManagementSystem.Domain.Entities
{
    [Table("Employees")]
    public class Employee
    {
        public int Id { get; set; }
        public required Name FullName { get; set; }
        public required DateOfBirth DateOfBirth { get; set; }
        public required Email Email { get; set; }
        public required PhoneNumber PhoneNumber { get; set; }
        public required Position Position { get; set; }
        public EmploymentStatus Status { get; set; }
        public required Department Department { get; set; }
        public EmployeeActivityStatus ActivityStatus { get; set; }
        public DateOnly DateHired { get; set; }
        public Decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}