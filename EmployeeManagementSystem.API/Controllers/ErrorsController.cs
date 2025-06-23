
using EmployeeManagementSystem.API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class ErrorsController : BaseApiController
    {
        [HttpGet("not-found")]
        public IActionResult GetNotFoundError()
        {
            return NotFound(BuildErrorWithContext(new ApiError
            {
                Title = "Resource not found",
                Status = StatusCodes.Status404NotFound,
                Detail = "The requested resource could not be found.",
            }));
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequestError()
        {
            return BadRequest(BuildErrorWithContext(new ApiError
            {
                Title = "Bad request provided",
                Status = StatusCodes.Status400BadRequest,
                Detail = "This is a bad request, please check your request.",
                Path = HttpContext.Request.Path,
            }));
        }

        [HttpGet("validation")]
        public IActionResult GetValidationError()
        {
            ModelState.AddModelError("Error 1", "This is First Error.");
            ModelState.AddModelError("Error 2", "This is Second Error.");
            return ValidationProblem();
        }

        [HttpGet("unauthorized")]
        public IActionResult GetUnAuthorizedError()
        {
            return Unauthorized(BuildErrorWithContext(new ApiError
            {
                Title = "Unauthorized access",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "The content you are trying to view is unauthorized.",
                Path = HttpContext.Request.Path,
            }));
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server error.");
        }
    }
}