using BookLibraryAPIDemo.Application.DTO.Author;
using BookLibraryAPIDemo.Application.DTO.category;
using BookLibraryAPIDemo.Application.DTO.Publisher;

namespace BookLibraryAPIDemo.Application.DTO.Book
{
    public class AllBooksDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public AuthorDTO Author { get; set; }
        public CategoryDTO Category { get; set; }
        public PublisherDTO Publisher { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
        public BookDetailDTO BookDetail { get; set; }
    }
}