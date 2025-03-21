namespace BookLibraryAPIDemo.Application.DTO;

public class CreateBookDetailDTO
{
    public string Description { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string BookId { get; set; }
}