using InventoryService.Domain.Stock;
using InventoryService.Domain.Warehouse;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Stock> Stocks { get; }
        public DbSet<WhereHouse> Whouses { get; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
