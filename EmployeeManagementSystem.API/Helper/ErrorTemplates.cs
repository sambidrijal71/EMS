using EmployeeManagementSystem.API.Errors;

namespace EmployeeManagementSystem.API.Helper
{
    public static class ErrorTemplates
    {
        public static ApiError NotFound(string? detail)
        {
            return new ApiError
            {
                Type = "https://httpstatuses.com/404",
                Title = "Resource not found",
                Status = StatusCodes.Status404NotFound,
                Detail = detail ?? "The requested resource could not be found.",
            };
        }

        public static ApiError BadRequest(string? detail)
        {
            return new ApiError
            {
                Type = "https://httpstatuses.com/400",
                Title = "Bad request provided",
                Status = StatusCodes.Status400BadRequest,
                Detail = detail ?? "This is a bad request, please check your request.",
            };
        }

        public static ApiError UnAuthorized(string? detail)
        {
            return new ApiError
            {
                Type = "https://httpstatuses.com/401",
                Title = "Unauthorized access",
                Status = StatusCodes.Status401Unauthorized,
                Detail = detail ?? "The content you are trying to view is unauthorized.",
            };
        }
        public static ApiError Conflict(string? detail)
        {
            return new ApiError
            {
                Type = "https://httpstatuses.com/409",
                Title = "Conflict",
                Status = StatusCodes.Status409Conflict,
                Detail = detail ?? "A conflict occurred while processing your request."
            };
        }

    }
}