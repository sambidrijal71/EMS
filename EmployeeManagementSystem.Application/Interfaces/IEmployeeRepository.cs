using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<ReadEmployeeDto>> GetAllEmployeesAsync();
        Task<ReadEmployeeDto?> GetEmployeeByIdAsync(int id);
        Task<ReadEmployeeDto> AddEmployeeAsync(CreateEmployeeDto employeeDto);
        Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDto employeeDto);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}