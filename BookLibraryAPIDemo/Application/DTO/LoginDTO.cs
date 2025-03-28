using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required"), RegularExpression("^(.+)@(.+)$")]
        public string Email { get; set; }
    }
}