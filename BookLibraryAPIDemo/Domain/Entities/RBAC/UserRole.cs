using Microsoft.AspNetCore.Identity;

namespace BookLibraryAPIDemo.Domain.Entities.RBAC;

public class UserRole 
{
    public required string UserId { get; set; }
    public required string RoleId { get; set; }
}