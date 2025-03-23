using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO;

public class CreateBookDetailDTO
{
    [Required] public string Description { get; set; }
    [Required] public string Title { get; set; }
    [Required] public double Price { get; set; }
    [Required] public int PageCount { get; set; }
}