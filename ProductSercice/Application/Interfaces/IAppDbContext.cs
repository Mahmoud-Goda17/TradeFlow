using Microsoft.EntityFrameworkCore;
using ProductSercice.Domain.Brand;
using ProductSercice.Domain.Category;
using ProductSercice.Domain.Product;

namespace ProductSercice.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Brand> brands { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<Product> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
