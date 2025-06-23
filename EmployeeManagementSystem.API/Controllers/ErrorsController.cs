using EmployeeManagementSystem.API.Helper;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Controllers
{
    public class ErrorsController : BaseApiController
    {
        [HttpGet("not-found")]
        public IActionResult GetNotFoundError()
        {
            return NotFound(ErrorTemplates.NotFound("The requested resource could not be found."));
        }

        [HttpGet("bad-request")]
        public IActionResult GetBadRequestError()
        {
            return BadRequest(ErrorTemplates.BadRequest("This is a bad request, please check your request."));
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
            return Unauthorized(ErrorTemplates.Unauthorized("The content you are trying to view is unauthorized."));
        }

        [HttpGet("server-error")]
        public IActionResult GetServerError()
        {
            throw new Exception("This is a server error.");
        }
    }
}