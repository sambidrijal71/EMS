using EmployeeManagementSystem.API.Modal;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Helper
{
    public class PaginatedList<T> : List<T>
    {
        public MetaData Metadata { get; set; }
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Metadata = new MetaData
            {
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                PageSize = pageSize,
                TotalCount = count
            };
            AddRange(items);
        }

        public static Task<PaginatedList<T>> ToPaginatedList(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var count = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return Task.FromResult(new PaginatedList<T>(items, count, pageNumber, pageSize));
        }
    }
}