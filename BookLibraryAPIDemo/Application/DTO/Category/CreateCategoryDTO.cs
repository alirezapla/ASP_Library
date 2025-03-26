using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO.category
{
    public class CreateCategoryDTO
    {
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
    }
}