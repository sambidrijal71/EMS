using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enums;
using EmployeeManagementSystem.Domain.ValueObjects;
using EmployeeManagementSystem.Infrastructure.Persistence;

namespace EmployeeManagementSystem.Infrastructure.SeedData
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (context.Employees.Any()) return;

            var employees = new List<Employee>
            {
new Employee
                {
                    FullName = new Name("John", "Doe"),
                    Email = new Email("john.doe@example.com"),
                    PhoneNumber = new PhoneNumber("0412345678"),
                    DateOfBirth = new DateOfBirth(DateOnly.Parse("1985-02-10")),
                    Position = Position.Ceo,
                    Status = EmploymentStatus.FullTime,
                    Department = Department.InformationTechnology,
                    ActivityStatus = EmployeeActivityStatus.Active,
                    DateHired = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-5)),
                    Salary = 150000,
                    CreatedAt = DateTime.UtcNow
                },
                new Employee
                {
                    FullName = new Name("Jane", "Smith"),
                    Email = new Email("jane.smith@example.com"),
                    PhoneNumber = new PhoneNumber("0498765432"),
                    DateOfBirth = new DateOfBirth(DateOnly.Parse("1990-06-25")),
                    Position = Position.SeniorDeveloper,
                    Status = EmploymentStatus.Contract,
                    Department = Department.HumanResource,
                    ActivityStatus = EmployeeActivityStatus.Inactive,
                    DateHired = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-2)),
                    Salary = 90000,
                    CreatedAt = DateTime.UtcNow
                }
            };
            await context.AddRangeAsync(employees);
            await context.SaveChangesAsync();
        }
    }
}