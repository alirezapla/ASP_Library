namespace BookLibraryAPIDemo.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}