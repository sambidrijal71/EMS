using System.Text.Json;
using EmployeeManagementSystem.API.Modal;

namespace EmployeeManagementSystem.API.Helper
{
    public static class HttpExtension
    {
        public static void FormatResponseHeader(this HttpResponse response, MetaData metaData)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(metaData, options));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}