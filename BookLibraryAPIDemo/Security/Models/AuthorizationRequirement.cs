using Microsoft.AspNetCore.Authorization;

namespace BookLibraryAPIDemo.Security.Models;

public sealed class AuthorizationRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}