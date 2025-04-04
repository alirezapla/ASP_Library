using BookLibraryAPIDemo.Security.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BookLibraryAPIDemo.Security;

public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy= await base.GetPolicyAsync(policyName);
        if (policy is not null)
        {
            return policy;
        }

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new AuthorizationRequirement(policyName))
            .Build();
    }
}