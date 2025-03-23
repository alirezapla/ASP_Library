using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; }
        [Required] public string AuthorId { get; set; }
        [Required] public string CategoryId { get; set; }
        [Required] public string PublisherId { get; set; }
    }
}