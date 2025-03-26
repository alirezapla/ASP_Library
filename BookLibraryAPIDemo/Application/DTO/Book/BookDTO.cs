using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO.Book
{
    public class BookDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string CategoryId { get; set; }
        public string PublisherId { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
        public string BookDetailId { get; set; }
    }
}