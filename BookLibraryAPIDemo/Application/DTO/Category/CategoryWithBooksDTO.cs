using BookLibraryAPIDemo.Application.DTO.Book;

namespace BookLibraryAPIDemo.Application.DTO.category
{
    public class CategoryWithBooksDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<IncludeBooksDTO> Books { get; set; }
    }
}