using Ardalis.SmartEnum;
using Microsoft.AspNetCore.Identity;

namespace BookLibraryAPIDemo.Domain.Entities.RBAC;

public class Role : IdentityRole<string>
{
    public ICollection<Permission> Permissions { get; set; } = default!;
    public ICollection<User> Users { get; set; } = default!;
}

// public class RoleEnum(int value, string name) : SmartEnum<RoleEnum>(name, value)
// {
//     public static readonly RoleEnum Admin = new (1,"Admin");
//     public static readonly RoleEnum User = new (2, "User");
// }