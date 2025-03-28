using Ardalis.SmartEnum;

namespace BookLibraryAPIDemo.Domain.Entities.RBAC;

public class Permission 
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public ICollection<Role>? Roles { get; set; }
}
public class PermissionEnum(string name, int value) : SmartEnum<PermissionEnum>(name, value)
{
    public static readonly PermissionEnum Read = new ("Read", 1);
    public static readonly PermissionEnum Write = new ("Write", 2);
}
