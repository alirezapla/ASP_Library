using BookLibraryAPIDemo.Security.Extensions;
using BookLibraryAPIDemo.Security.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookLibraryAPIDemo.Security;

public class CustomAuthorizationHandler : AuthorizationHandler<AuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthorizationRequirement requirement)
    {
        var permissions = context.User.GetPermissions();
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}