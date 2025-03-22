using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO;

public class CreateReviewDTO
{
    [Required] public string Rating { get; set; }
    [Required] public string Comment { get; set; }
}