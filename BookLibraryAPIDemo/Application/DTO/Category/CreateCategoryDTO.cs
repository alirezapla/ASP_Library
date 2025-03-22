using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO
{
    public class CreateCategoryDTO
    {
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
    }
}