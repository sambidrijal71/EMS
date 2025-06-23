using System.Net;
using System.Text.Json;
using EmployeeManagementSystem.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/problem+json";

                var (statusCode, title) = ex switch
                {
                    UserAuthenticationException => ((int)HttpStatusCode.BadRequest, "Authentication Error"),
                    // EmployeeFetchException => ((int)HttpStatusCode.InternalServerError, "Employee Fetch Error"),
                    _ => ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred")
                };
                context.Response.StatusCode = statusCode;

                var problemDetails = new ProblemDetails
                {
                    Type = $"https://httpstatuses.com/{statusCode}",
                    Title = _environment.IsDevelopment() ? ex.Message : title,
                    Status = statusCode,
                    Detail = _environment.IsDevelopment() ? ex.StackTrace : "Please contact the admin",
                    Instance = context.Request.Path
                };

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(problemDetails, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}