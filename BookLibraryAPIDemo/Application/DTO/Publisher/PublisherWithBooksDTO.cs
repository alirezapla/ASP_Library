using BookLibraryAPIDemo.Application.DTO.Book;

namespace BookLibraryAPIDemo.Application.DTO.Publisher;

public class PublisherWithBooksDto
{
    public string Id { get; set; }
    public string PublisherName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public ICollection<IncludeBooksDTO> Books { get; set; }
}