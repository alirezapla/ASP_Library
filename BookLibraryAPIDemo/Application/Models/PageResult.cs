using BookLibraryAPIDemo.Application.Models;

namespace BookLibraryAPIDemo.Infrastructure.Repositories;

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int) Math.Ceiling(TotalCount / (double) PageSize);

    public PagedResult(List<T> items, int totalCount, PaginationParams paginationParams)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = paginationParams.Number;
        PageSize = paginationParams.Size;
    }
}