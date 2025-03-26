using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO.Publisher
{
    public class CreatePublisherDTO
    {
        [Required] public string PublisherName { get; set; }
        [Required] public string Address { get; set; }
        [Required] public string City { get; set; }
        [Required] public string Country { get; set; }
    }
}