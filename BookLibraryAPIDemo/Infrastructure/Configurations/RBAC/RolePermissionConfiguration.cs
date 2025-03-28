using BookLibraryAPIDemo.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPIDemo.Infrastructure.Configurations.RBAC;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");
        builder.HasKey(rp => new {rp.RoleId, rp.PermissionId});
        builder.HasData(Create("0021877C-3F5F-45E3-BE2C-A339601532B6", PermissionEnum.Read),
            Create("0021877C-3F5F-45E3-BE2C-A339601532B6", PermissionEnum.Write),
            Create("4AA5C7A2-9769-4D6D-B0FB-4C1EE0CD7B22", PermissionEnum.Read));
    }

    private static RolePermission Create(string role, PermissionEnum permission)
    {
        return new RolePermission
        {
            PermissionId = permission.Value,
            RoleId = role
        };
    }
}