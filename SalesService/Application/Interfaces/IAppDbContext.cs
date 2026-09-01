using Microsoft.EntityFrameworkCore;
using SalesService.Domain.SalesOrderItems;
using SalesService.Domain.SalesOrders;

namespace SalesService.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<SalesOrder> Sales { get; }
        public DbSet<SalesOrderItem> items { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
