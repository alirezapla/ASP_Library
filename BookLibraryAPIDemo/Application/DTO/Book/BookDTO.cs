namespace BookLibraryAPIDemo.Application.DTO
{
    public class BookDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public double Price { get; set; }
        public string CategoryId { get; set; }
        public string PublisherId { get; set; }
        public List<ReviewDTO> Review { get; set; }
        public string BookDetailId { get; set; }
    }
}