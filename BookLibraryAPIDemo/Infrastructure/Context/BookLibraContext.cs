using System.Reflection;
using BookLibraryAPIDemo.Domain.Entities;
using BookLibraryAPIDemo.Domain.Entities.RBAC;
using BookLibraryAPIDemo.Infrastructure.Interfaces;
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
        public DbSet<User> Users { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (!typeof(IEntity).IsAssignableFrom(entityType.ClrType)) continue;
                var method = typeof(BookLibraryContext)
                    .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(null, new object[] {builder});
            }

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

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class, IEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
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
                        if (entity.IsDeleted)
                        {
                            entity.IsDeleted = true;
                            entity.DeletedDate = DateTime.UtcNow;
                            entity.DeletedBy = currentUser;
                            break;
                        }

                        entity.UpdatedDate = DateTime.UtcNow;
                        entity.UpdatedBy = currentUser;
                        break;
                }
            }
        }
    }
}