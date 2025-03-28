using BookLibraryAPIDemo.Application.Commands.Auth;
using BookLibraryAPIDemo.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPIDemo.Controllers
{
    public class AuthController : BaseApiController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO model)
        {
            var result = await Mediator.Send(new RegisterUser {Model = model});

            if (result.Succeeded)
            {
                return Ok(new {Message = "User account created successfully"});
            }
            else
            {
                return BadRequest(new {Errors = result.Errors});
            }
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            return Ok(await Mediator.Send(new Login {LoginDTO = model}));
        }
    }
}