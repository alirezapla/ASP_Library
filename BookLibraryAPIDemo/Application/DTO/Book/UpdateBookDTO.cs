namespace BookLibraryAPIDemo.Application.DTO.Book
{
    public class UpdateBookDTO
    {
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string CategoryId { get; set; }
        public string PublisherId { get; set; }
    }
}