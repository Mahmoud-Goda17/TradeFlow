using Microsoft.EntityFrameworkCore;
using SalesRead.Application.Interfaces;
using SalesRead.Damain.SalesOrderProductReadModels;

namespace SalesRead.InfraStrucure.Data
{
    public class AppDpContext : DbContext, IAppDbContext
    {
        public AppDpContext(DbContextOptions<AppDpContext> options) : base(options)
        {
        }
        public DbSet<SalesOrderProductReadModel> SalesOrderProductReadModels => Set<SalesOrderProductReadModel>();
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
