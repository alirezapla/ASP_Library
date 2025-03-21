using BookLibraryAPIDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPIDemo.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> entity)
    {
        entity.HasKey(r => r.Id);
        entity.HasOne(r => r.Book)
            .WithMany(r => r.Reviews)
            .HasForeignKey(r => r.BookId);
    }
}