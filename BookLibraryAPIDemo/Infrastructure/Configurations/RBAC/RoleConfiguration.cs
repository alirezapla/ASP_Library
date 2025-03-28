using BookLibraryAPIDemo.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPIDemo.Infrastructure.Configurations.RBAC;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
        builder.HasMany<Permission>(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>();
        builder.HasMany<User>(r => r.Users)
            .WithMany(u => u.Roles)
            .UsingEntity<UserRole>();
        builder.HasData(
            new Role
            {
                Id = "0021877C-3F5F-45E3-BE2C-A339601532B6",
                Name = "Admin"
            },
            new Role
            {
                Id = "4AA5C7A2-9769-4D6D-B0FB-4C1EE0CD7B22",
                Name = "User"
            }
        );
    }
}