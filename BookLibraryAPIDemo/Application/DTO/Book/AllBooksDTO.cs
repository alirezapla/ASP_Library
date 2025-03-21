namespace BookLibraryAPIDemo.Application.DTO
{
    public class AllBooksDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
        public double Price { get; set; }
        public CategoryDTO Category { get; set; }
        public PublisherDTO Publisher { get; set; }
        public List<ReviewDTO> Review { get; set; }
        public BookDetailDTO BookDetail { get; set; }
    }
}