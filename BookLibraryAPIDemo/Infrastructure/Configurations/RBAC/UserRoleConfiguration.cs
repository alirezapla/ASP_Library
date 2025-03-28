using BookLibraryAPIDemo.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPIDemo.Infrastructure.Configurations.RBAC;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");
        builder.HasKey(ur => new {ur.UserId, ur.RoleId});
        builder.HasData(new UserRole()
        {
            UserId = "32F8F8D1-5510-45D9-9B39-29A35FDD85EC",
            RoleId = "0021877C-3F5F-45E3-BE2C-A339601532B6"
        });
    }
}