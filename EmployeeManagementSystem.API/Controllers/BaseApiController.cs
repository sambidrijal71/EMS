using EmployeeManagementSystem.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ApiError BuildErrorWithContext(ApiError apiError)
        {
            return new ApiError
            {
                Type = apiError.Type,
                Title = apiError.Title,
                Status = apiError.Status,
                Detail = apiError.Detail,
                Path = HttpContext.Request.Path,
            };
        }
    }
}