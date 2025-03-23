namespace BookLibraryAPIDemo.Domain.Entities;

public class Review : BaseEntity
{
    public int Rating { get; set; } 
    public string Comment { get; set; }
    public string Caption { get; set; }
    // public User Reviewer { get; set; }
    public Book Book { get; set; }
    public string BookId { get; set; }
}