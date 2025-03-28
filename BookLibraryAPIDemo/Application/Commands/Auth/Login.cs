using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookLibraryAPIDemo.Application.Exceptions;
using BookLibraryAPIDemo.Domain.Entities.RBAC;
using BookLibraryAPIDemo.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Application.Commands.Auth
{
    public class Login : IRequest<UserPrincipalDTO>
    {
        public LoginDTO LoginDTO { get; set; }


        public class LoginHandler : IRequestHandler<Login, UserPrincipalDTO>
        {
            private readonly ILoggerManager _logger;
            private readonly IConfiguration _configuration;
            private readonly BookLibraryContext _context;
            private readonly UserManager<User> _userManager;

            public LoginHandler(ILoggerManager logger, IConfiguration configuration, BookLibraryContext context,
                UserManager<User> userManager)
            {
                _logger = logger;
                _configuration = configuration;
                _context = context;
                _userManager = userManager;
            }


            /// <summary>
            /// Login Handler 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<UserPrincipalDTO> Handle(Login request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(u => u.Roles)
                    .FirstOrDefaultAsync(u => u.Email == request.LoginDTO.Email, cancellationToken: cancellationToken);
                if (user == null || !(await _userManager.CheckPasswordAsync(user, request.LoginDTO.Password)))
                {
                    _logger.LogWarn($"{nameof(Login)}: Login failed. Wrong email or password");
                    throw new LogInFailedException("Login failed. Wrong email or password");
                }

                var token = await CreateToken(user);
                return new UserPrincipalDTO
                {
                    Message = "User logged in successfully",
                    Username = user.UserName,
                    Email = user.Email,
                    Token = token
                };
            }


            /// <summary>
            ///  Creating token 
            /// </summary>
            /// <returns></returns>
            public async Task<string> CreateToken(User user)
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaimsAsync(user);
                var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

                return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            }

            private SigningCredentials GetSigningCredentials()
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var key = Encoding.UTF8.GetBytes(jwtSettings["key"]);
                var secret = new SymmetricSecurityKey(key);

                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }

            private async Task<List<Claim>> GetClaimsAsync(User user)
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Sid, user.Id),
                    new Claim(ClaimTypes.Dns, user.Dns),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString("yyyy-MM-dd"))
                };
                foreach (var userRole in user.Roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole.Name!));
                }

                return authClaims;
            }

            /// <summary>
            ///  Generate tooken options 
            /// </summary>
            /// <param name="signingCredentials"></param>
            /// <param name="claims"></param>
            /// <returns></returns>
            private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
            {
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var tokenOptions = new JwtSecurityToken
                (
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signingCredentials
                );

                return tokenOptions;
            }
        }
    }
}