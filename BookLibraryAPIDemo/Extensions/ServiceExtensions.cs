using System.Reflection;
using System.Security.Claims;
using BookLibraryAPIDemo.Application.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookLibraryAPIDemo.API.Extensions;
using BookLibraryAPIDemo.Application.Behaviors;
using BookLibraryAPIDemo.Application.Commands.BookLibraryAPICategory;
using BookLibraryAPIDemo.Application.Mapping;
using BookLibraryAPIDemo.Application.Services;
using BookLibraryAPIDemo.Domain.Entities.RBAC;
using BookLibraryAPIDemo.Infrastructure.Filters;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
using BookLibraryAPIDemo.Infrastructure.Repositories;
using BookLibraryAPIDemo.Security;
using BookLibraryAPIDemo.Security.Extensions;
using BookLibraryAPIDemo.Security.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;

namespace BookLibraryAPIDemo.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(o => { o.UseRoutePrefix("api"); });

            services.AddHttpContextAccessor();

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddScoped<LogActionFilter>();
            services.AddScoped<CustomAuthorizationFilter>();

            services.AddDbContext<BookLibraryContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                    b => b.MigrationsAssembly("BookLibraryAPIDemo"));
            });

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddMemoryCache();
            services.AddSingleton<MemoryCacheEntryOptions>(provider =>
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Cache for 10 minutes
                });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            services.AddMediatR(m => m.RegisterServicesFromAssemblyContaining(typeof(CreateCategory)));

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.ConfigureCors(configuration);

            services.ConfigureIdentity();
            services.AddSystemAuthorization();

            services.ConfigureJwt(configuration);

            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument();

            services.ConfigureSwagger();
        }

        private static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() {Title = "You api title", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }


        private static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(
                options => options.AddPolicy(
                    "CorsPolicy",
                    policy => policy.WithOrigins(
                            configuration["BackendUrl"] ?? "https://localhost:5052")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                )
            );
        }

        private static IServiceCollection AddSystemAuthorization(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddAuthorization(options =>
            {
                options.AddPolicy("CanRead",
                    policy =>
                    {
                        policy.Requirements.Add(new AuthorizationRequirement("Read"));
                    });
                options.AddPolicy("CanWrite",
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new AuthorizationRequirement("Write"));
                    });

                options.AddPolicy("MinimumAge", policy =>
                    policy.RequireClaim("DateOfBirth")
                        .RequireAssertion(context =>
                        {
                            var dob = DateTime.Parse(context.User.FindFirstValue("DateOfBirth"));
                            return DateTime.Now.Year - dob.Year >= 18;
                        }));
            });
            serviceProvider.AddScoped<IClaimsTransformation, CustomClaimsTransformation>();
            serviceProvider.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
            serviceProvider.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

            return serviceProvider;
        }

        private static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(o =>
                {
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireDigit = true;
                    o.Password.RequireLowercase = true;
                    o.Password.RequireUppercase = false;
                    o.Password.RequiredLength = 8;
                }).AddEntityFrameworkStores<BookLibraryContext>()
                .AddDefaultTokenProviders();
        }

        private static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["key"];

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
        }
    }
}