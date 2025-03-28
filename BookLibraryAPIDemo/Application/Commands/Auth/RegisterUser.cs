using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities.RBAC;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookLibraryAPIDemo.Application.Commands.Auth
{
    public class RegisterUser : IRequest<IdentityResult>
    {
        public RegisterUserDTO Model { get; set; }
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUser, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RoleManager<Role> _roleManager;


        public RegisterUserHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            var userExists = await _userManager.FindByNameAsync(request.Model.Username);
            if (userExists != null)
                throw new UserRegistrationException("User already exists");

            await CreateUser(request);

            return IdentityResult.Success;
        }

        private async Task CreateUser(RegisterUser request)
        {
            if (!await _roleManager.RoleExistsAsync(request.Model.Role))
                throw new UserRegistrationException("Role does not exist");

            var user = new User
            {
                UserName = request.Model.Username,
                Email = request.Model.Email,
                Dns = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Model.Password);
            if (!createUserResult.Succeeded)
                throw new UserRegistrationException("User creation failed! Please check user details and try again.");

            var roleResult = await _userManager.AddToRoleAsync(user, request.Model.Role);
            if (!roleResult.Succeeded)
            {
                throw new UserRegistrationException("Failed to register user");
            }
        }
    }
}