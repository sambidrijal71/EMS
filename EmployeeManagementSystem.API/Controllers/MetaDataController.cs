using EmployeeManagementSystem.Domain.Enums;
using EmployeeManagementSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Controllers
{
    public class MetaDataController : BaseApiController
    {

        [HttpGet("positions")]
        public IActionResult GetPositions() => Ok(EnumHelper.ToList<Position>());

        [HttpGet("department")]
        public IActionResult GetDepartments() => Ok(EnumHelper.ToList<Department>());

        [HttpGet("employee-activity-status")]
        public IActionResult GetEmployeeActivityStatus() => Ok(EnumHelper.ToList<EmployeeActivityStatus>());

        [HttpGet("employment-status")]
        public IActionResult GetEmployMentStatus() => Ok(EnumHelper.ToList<EmploymentStatus>());
    }
}