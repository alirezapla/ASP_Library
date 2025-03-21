using BookLibraryAPIDemo.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryAPIDemo.Infrastructure.Context
{
    public class BookLibraryContext : IdentityDbContext<IdentityUser>

    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookLibraryContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(BookLibraryContext).Assembly);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity) entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = DateTime.UtcNow;
                        entity.CreatedBy = currentUser;
                        entity.UpdatedDate = null;
                        entity.UpdatedBy = "";
                        entity.IsDeleted = false;
                        entity.DeletedDate = null;
                        entity.DeletedBy = "";
                        break;
                    case EntityState.Modified:
                        entity.UpdatedDate = DateTime.UtcNow;
                        entity.UpdatedBy = currentUser;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entity.IsDeleted = true;
                        entity.DeletedDate = DateTime.UtcNow;
                        entity.DeletedBy = currentUser;
                        break;
                }
            }
        }
    }
}