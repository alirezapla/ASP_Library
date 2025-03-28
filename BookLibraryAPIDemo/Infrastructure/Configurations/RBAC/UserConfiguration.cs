using BookLibraryAPIDemo.Domain.Entities.RBAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPIDemo.Infrastructure.Configurations.RBAC;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("Users");
        entity.Property(u => u.Email).IsRequired();
        entity.HasIndex(u => new {u.Email, u.IsDeleted})
            .IsUnique();
        entity.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRole>();
        entity.HasData(new User()
        {
            Id = "32F8F8D1-5510-45D9-9B39-29A35FDD85EC",
            Email = "a@h.c",
            UserName = "Admin",
            DateOfBirth = DateTime.Parse("1989-04-05"),
            CreatedDate = DateTime.UtcNow,
            CreatedBy = "system",
            UpdatedDate = null,
            UpdatedBy = "",
            IsDeleted = false,
            DeletedBy = "",
        });
    }
}