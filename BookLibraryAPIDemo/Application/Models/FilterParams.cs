namespace BookLibraryAPIDemo.Application.Models;

public class FilterParams
{
    public required string PropertyName { get; set; }
    public required string PropertyValue { get; set; }
    public required string Operator { get; set; }
}