namespace BookLibraryAPIDemo.Domain.Entities;

public class Review : BaseEntity
{
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int Rating { get; set; } 
    public string Comment { get; set; }
}