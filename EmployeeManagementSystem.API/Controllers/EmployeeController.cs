using EmployeeManagementSystem.API.Helper;
using EmployeeManagementSystem.API.Modal;
using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class EmployeeController : BaseApiController
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadEmployeeDto>>> GetAllEmployees([FromQuery] EmployeeParams employeeParams)
        {
            var query = await _employeeRepository.GetAllEmployeesAsync();
            query.SortEmployee(employeeParams.SortBy).SearchByKeyword(employeeParams.SearchKey);
            var queryableQuery = query.AsQueryable();
            var employees = await PaginatedList<ReadEmployeeDto>.ToPaginatedList(queryableQuery, employeeParams.CurrentPage, employeeParams.PageSize);
            Response.FormatResponseHeader(employees.Metadata);
            return Ok(employees.ToList());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadEmployeeDto>> GetEmployee(int id)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var result = await _employeeRepository.AddEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, UpdateEmployeeDto employeeDto)
        {
            var result = await _employeeRepository.UpdateEmployeeAsync(id, employeeDto);
            if (!result) return NotFound(ErrorTemplates.NotFound("New data and existing data cannot be same."));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeRepository.DeleteEmployeeAsync(id);
            if (!result) return BadRequest(ErrorTemplates.BadRequest($"Employee with id {id} does not exist."));
            return NoContent();
        }
    }
}