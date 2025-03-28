using System.Security.Claims;
using BookLibraryAPIDemo.Security.Models;

namespace BookLibraryAPIDemo.Security.Extensions;

public static class ClaimsExtensions
{
    public static HashSet<string> GetPermissions(this ClaimsPrincipal user)
    {
        // Define the claim type for permissions
       

        // Retrieve all claims of the specified type
        IEnumerable<Claim> permissionClaims = user?.FindAll(CustomClaims.Permission) ?? new List<Claim>();

        var permissions = new HashSet<string>();
        foreach (var claim in permissionClaims)
        {
            if (!string.IsNullOrWhiteSpace(claim.Value))
            {
                permissions.Add(claim.Value);
            }
        }

        return permissions;
    }
}