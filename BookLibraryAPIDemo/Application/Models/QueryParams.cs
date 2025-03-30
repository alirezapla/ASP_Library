namespace BookLibraryAPIDemo.Application.Models;

public sealed class QueryParams
{
    public PaginationParams PaginationParams { get; set; }
    public SortParams SortParams { get; set; }
    public List<FilterParams> Filters { get; set; }
}