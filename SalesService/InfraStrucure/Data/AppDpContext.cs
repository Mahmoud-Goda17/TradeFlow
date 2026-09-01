using Microsoft.EntityFrameworkCore;
using SalesService.Application.Interfaces;
using SalesService.Domain.SalesOrderItems;
using SalesService.Domain.SalesOrders;

namespace SalesService.InfraStrucure.Data
{
    public class AppDpContext : DbContext, IAppDbContext
    {

        public AppDpContext(DbContextOptions<AppDpContext> options) : base(options)
        {
        }
        public DbSet<SalesOrder> Sales => Set<SalesOrder>();

        public DbSet<SalesOrderItem> items => Set<SalesOrderItem>();

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
