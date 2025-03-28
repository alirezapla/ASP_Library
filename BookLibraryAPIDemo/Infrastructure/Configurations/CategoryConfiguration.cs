using BookLibraryAPIDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPIDemo.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired();
            entity.HasIndex(e => new {e.Name, e.IsDeleted})
                .IsUnique();
            entity.HasData(
                new Category
                {
                    Id = "470389e2-4c68-4efd-850a-ed3282ae236e",
                    Name = "Tech",
                    Description = "This is about Tech",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "system",
                    UpdatedDate = null,
                    UpdatedBy = "",
                    IsDeleted = false,
                    DeletedBy = "",
                },
                new Category
                {
                    Id = "8fece538-ab92-4316-9c19-6693514dc283",
                    Name = "Finance ",
                    Description = "Books on Finance ",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "system",
                    UpdatedDate = null,
                    UpdatedBy = "",
                    IsDeleted = false,
                    DeletedBy = "",
                },
                new Category
                {
                    Id = "bd97c0cc-7e68-4a59-935d-8d7d12269cbe",
                    Name = "Science",
                    Description = "Books on science and nature",
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = "system",
                    UpdatedDate = null,
                    UpdatedBy = "",
                    IsDeleted = false,
                    DeletedBy = "",
                }
            );
        }
    }
}