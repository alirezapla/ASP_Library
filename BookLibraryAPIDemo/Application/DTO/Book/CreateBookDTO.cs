namespace BookLibraryAPIDemo.Application.DTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public double Price { get; set; }
        public string CategoryId { get; set; }
        public string PublisherId { get; set; }
    }
}