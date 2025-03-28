using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPIDemo.Application.DTO
{
    public class RegisterUserDTO
    {
        [Required] public string Username { get; set; }

        [Required, RegularExpression("^(.+)@(.+)$")]
        public string Email { get; set; }

        [Required] public string Password { get; set; }
        
        [Required] public string DateOfBirth { get; set; }
        [Required] public string Role { get; set; }
    }
}