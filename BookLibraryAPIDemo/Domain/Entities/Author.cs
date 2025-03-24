namespace BookLibraryAPIDemo.Domain.Entities
{

    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public List<Book> Books { get; set; }
    }

}