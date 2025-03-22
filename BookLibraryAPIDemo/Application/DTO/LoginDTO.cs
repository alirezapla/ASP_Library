using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO
{
    public class LoginDTO
    {
        [Required] public string Password { get; set; }

        [Required, RegularExpression("^(.+)@(.+)$")]
        public string Email { get; set; }
    }
}