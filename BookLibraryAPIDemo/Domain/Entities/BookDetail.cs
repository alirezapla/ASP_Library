namespace BookLibraryAPIDemo.Domain.Entities;

public class BookDetail : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int PageCount { get; set; }
    public double Price { get; set; }

    public string BookId { get; set; }
    public Book Book { get; set; }
}