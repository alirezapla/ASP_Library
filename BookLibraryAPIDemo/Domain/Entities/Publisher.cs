namespace BookLibraryAPIDemo.Domain.Entities
{
    public class Publisher: BaseEntity
    {
        public string PublisherName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Book> Books { get; set; }
    }
}