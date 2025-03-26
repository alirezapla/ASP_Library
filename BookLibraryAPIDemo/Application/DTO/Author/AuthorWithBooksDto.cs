namespace BookLibraryAPIDemo.Application.DTO.Author
{
    public class AuthorWithBooksDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public ICollection<IncludeBooksDTO> Books { get; set; }
    }
}