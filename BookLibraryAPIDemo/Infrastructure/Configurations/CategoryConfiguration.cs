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
            entity.HasIndex(e => new {e.Name,e.IsDeleted})
                .IsUnique();
            entity.HasData(
                new Category
                {
                    Id = Guid.NewGuid().ToString(),
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
                    Id = Guid.NewGuid().ToString(),
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
                    Id = Guid.NewGuid().ToString(),
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