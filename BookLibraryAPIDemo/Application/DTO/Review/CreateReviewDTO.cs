using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO;

public class CreateReviewDTO
{
    [Required] public int Rating { get; set; }
    [Required] public string Caption { get; set; }
    [Required] public string Comment { get; set; }
}