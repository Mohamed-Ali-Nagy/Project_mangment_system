using Microsoft.EntityFrameworkCore;

namespace Project_management_system.ViewModels
{
    public class PaginatedList<T>
    {
        public List<T> Items { get;  set; }
        public int TotalCount { get;  set; }
        public int PageSize { get;  set; }
        public int CurrentPage { get;  set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public PaginatedList(List<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int currentPage, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, totalCount, currentPage, pageSize);
        }
    }


}
