using BookLibraryAPIDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPIDemo.Infrastructure.Configurations;

public class BookDetailConfiguration: IEntityTypeConfiguration<BookDetail>
{
    public void Configure(EntityTypeBuilder<BookDetail> entity)
    {
        entity.HasKey(b => b.Id);
        entity.Property(bd => bd.Description)
            .IsRequired()
            .HasMaxLength(1000);

        entity.Property(bd => bd.PageCount)
            .IsRequired();
    }
}