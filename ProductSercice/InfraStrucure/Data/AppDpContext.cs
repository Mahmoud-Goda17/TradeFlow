using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Interfaces;
using ProductSercice.Domain.Brand;
using ProductSercice.Domain.Category;
using ProductSercice.Domain.Product;

namespace ProductSercice.InfraStrucure.Data
{
    public class AppDpContext : DbContext, IAppDbContext
    {
        public AppDpContext(DbContextOptions<AppDpContext> options) : base(options)
        {
        }
        public DbSet<Brand> brands => Set<Brand>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        Task<int> IAppDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDpContext).Assembly);
        }
    }
}
