namespace BookLibraryAPIDemo.Domain.Entities;

public class BookDetails
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int PageCount { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
}