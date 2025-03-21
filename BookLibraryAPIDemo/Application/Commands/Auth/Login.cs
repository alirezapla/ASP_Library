﻿using BookLibraryAPIDemo.Application.DTO;
using BookLibraryAPIDemo.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookLibraryAPIDemo.Application.Commands.BookLibraryAPIAuth
{
    public class Login : IRequest<object>
    {
        public required LoginDTO LoginDTO { get; set; }


        public class LoginHandler : IRequestHandler<Login, object>
        {

            private IdentityUser _user;
            private readonly UserManager<IdentityUser> _userManager;
            private readonly ILoggerManager _logger;
            private readonly IConfiguration _configuration;


            public LoginHandler(UserManager<IdentityUser> userManager, ILoggerManager logger, IConfiguration configuration)
            {
                _userManager = userManager;
                _logger = logger;
                _configuration = configuration;
            }


            /// <summary>
            /// Login Handler 
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<object> Handle(Login request, CancellationToken cancellationToken)
            {
                _user = await _userManager.FindByEmailAsync(request.LoginDTO.Email);
                if (_user == null || !(await _userManager.CheckPasswordAsync(_user, request.LoginDTO.Password)))
                {
                    _logger.LogWarn($"{nameof(Login)}: Login failed. Wrong email or password");
                    return new { Message = "Login failed. Wrong email or password" };
                }
                var token = await CreateToken();
                return new
                {
                    Message = "User logged in successfully",
                    Username = _user.UserName,
                    Email = _user.Email,
                    Token = token
                };
            }




            /// <summary>
            ///  Creating token 
            /// </summary>
            /// <returns></returns>
            public async Task<string> CreateToken()
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaimsAsync();
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
            private async Task<List<Claim>> GetClaimsAsync()
            {
                var claims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, _user.UserName)
            };

                return claims;

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