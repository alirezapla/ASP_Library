namespace BookLibraryAPIDemo.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public BookDetail BookDetail { get; set; }
    }
}