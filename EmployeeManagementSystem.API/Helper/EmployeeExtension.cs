using EmployeeManagementSystem.Application.DTOs;
using EmployeeManagementSystem.Domain.Enums;

namespace EmployeeManagementSystem.API.Helper
{
    public static class EmployeeExtension
    {
        public static IEnumerable<ReadEmployeeDto> SortEmployee(this IEnumerable<ReadEmployeeDto> query, string? queryParams)
        {
            if (String.IsNullOrEmpty(queryParams))
            {
                query = query.OrderBy(q => q.FirstName);
            }
            switch (queryParams)
            {
                case "asc_fname":
                    {
                        query = query.OrderBy(q => q.FirstName);
                        break;

                    }
                case "asc_lname":
                    {
                        query = query.OrderBy(q => q.LastName);
                        break;
                    }
                case "asc_position":
                    {
                        query = query.OrderBy(q => q.Position);
                        break;
                    }
                case "asc_department":
                    {
                        query = query.OrderBy(q => q.Department);
                        break;
                    }
                case "asc_activitystatus":
                    {
                        query = query.OrderBy(q => q.ActivityStatus);
                        break;
                    }
                case "desc_fname":
                    {
                        query = query.OrderByDescending(q => q.FirstName);
                        break;
                    }
                case "desc_lname":
                    {
                        query = query.OrderByDescending(q => q.LastName);
                        break;
                    }
                case "desc_position":
                    {
                        query = query.OrderByDescending(q => q.Position);
                        break;
                    }
                case "desc_department":
                    {
                        query = query.OrderByDescending(q => q.Department);
                        break;
                    }
                case "desc_activitystatus":
                    {
                        query = query.OrderByDescending(q => q.ActivityStatus);
                        break;
                    }
                default:
                    {
                        query = query.OrderBy(q => q.FirstName);
                        break;
                    }
            }
            return query;
        }

        public static IEnumerable<ReadEmployeeDto> SearchByKeyword(this IEnumerable<ReadEmployeeDto> query, string? searchKey)
        {
            if (String.IsNullOrEmpty(searchKey)) return query;
            var lowerTermSearchKey = searchKey.Trim().ToLower();

            query = query.Where(q =>
            q.FirstName.ToLower().Contains(lowerTermSearchKey) ||
            q.LastName.ToLower().Contains(lowerTermSearchKey) ||
            q.Email.ToLower().Contains(lowerTermSearchKey));
            return query;
        }

    }
}