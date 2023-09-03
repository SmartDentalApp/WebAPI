using smart_dental_webapi.Entity;
using smart_dental_webapi.Models.Pagination;

namespace smart_dental_webapi.Helper
{
    public class PaginationHelper
    {
        public static List<T> Paginate<T>(IQueryable<T> dataSource, int page, int pageSize)
        {
            return dataSource
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public static void SetPaginationHeaders(IHeaderDictionary responseHeaders, Pagination paginationParameters, List<CustomerEntity> customers)
        {
            int totalItemCount = customers.Count();
            int totalPages = (int)Math.Ceiling((double)totalItemCount / paginationParameters.PageSize);

            responseHeaders.Add("X-Total-Count", totalItemCount.ToString());
            responseHeaders.Add("X-Total-Pages", totalPages.ToString());
        }
    }
}
