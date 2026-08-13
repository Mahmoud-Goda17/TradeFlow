using InventoryService.Application.Interfaces;
using InventoryService.Domain.Stock;
using InventoryService.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.InfraStrucure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Stock> Stocks => Set<Stock>();

        public DbSet<WhereHouse> Whouses => Set<WhereHouse>();
        Task<int> IAppDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

    }
}
