using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO;

public class UpdateBookDetailDto
{
    [Required] public string Description { get; set; }
    public double Price { get; set; }

}