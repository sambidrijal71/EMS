using EmployeeManagementSystem.Application.Interfaces;
using EmployeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Domain.ValueObjects;
using EmployeeManagementSystem.Domain.Enums;
using EmployeeManagementSystem.Application.Exceptions;
namespace EmployeeManagementSystem.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ReadEmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(i => i.Id == id);
            if (employee == null) throw new DllNotFoundException($"Employee with id {id} not Found");
            return MapToDto(employee);
        }

        public async Task<IEnumerable<ReadEmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            if (employees.Count == 0) throw new DllNotFoundException("No Employees in the database.");
            var employeeDtos = employees.Select(MapToDto).AsQueryable();

            return employeeDtos;
        }

        public async Task<ReadEmployeeDto> AddEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var existingEmployee = await _context.Employees.FirstOrDefaultAsync(i => i.Email.Value == employeeDto.Email || i.PhoneNumber.Value == employeeDto.PhoneNumber);
            if (existingEmployee != null) throw new ConflictException("Email or Phone Number already exists. Please try another.");

            var newEmployee = new Employee
            {
                FullName = new Name(employeeDto.FirstName, employeeDto.LastName),
                Email = new Email(employeeDto.Email),
                PhoneNumber = new PhoneNumber(employeeDto.PhoneNumber),
                DateOfBirth = new DateOfBirth(DateOnly.Parse(employeeDto.DateOfBirth, System.Globalization.CultureInfo.InvariantCulture)),
                Position = employeeDto.Position,
                Status = employeeDto.Status,
                Department = employeeDto.Department,
                ActivityStatus = employeeDto.ActivityStatus,
                DateHired = employeeDto.DateHired,
                Salary = employeeDto.Salary,
                CreatedAt = DateTime.UtcNow
            };
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
            return MapToDto(newEmployee);
        }

        public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null) throw new DllNotFoundException($"Employee with id {id} not Found");

            existingEmployee.FullName = new Name(employeeDto.FirstName, employeeDto.LastName);
            existingEmployee.DateOfBirth = new DateOfBirth(DateOnly.Parse(employeeDto.DateOfBirth, System.Globalization.CultureInfo.InvariantCulture));
            existingEmployee.Email = new Email(employeeDto.Email) ?? existingEmployee.Email;
            existingEmployee.PhoneNumber = new PhoneNumber(employeeDto.PhoneNumber);
            existingEmployee.Position = employeeDto.Position;
            existingEmployee.Status = employeeDto.Status;
            existingEmployee.Department = employeeDto.Department;
            existingEmployee.ActivityStatus = employeeDto.ActivityStatus;
            existingEmployee.DateHired = employeeDto.DateHired;
            existingEmployee.Salary = employeeDto.Salary;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null) return false;
            _context.Remove(existingEmployee);
            var result = await _context.SaveChangesAsync();
            return result > 0;

        }

        private ReadEmployeeDto MapToDto(Employee employee)
        {
            return new ReadEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FullName.FirstName,
                LastName = employee.FullName.LastName,
                Email = employee.Email.Value,
                PhoneNumber = employee.PhoneNumber.Value,
                Position = employee.Position.GetDescription(),
                Status = employee.Status.GetDescription(),
                Department = employee.Department.GetDescription(),
                ActivityStatus = employee.ActivityStatus.GetDescription(),
            };
        }
    }
}