using Microsoft.EntityFrameworkCore;
using SalesRead.Damain.SalesOrderProductReadModels;

namespace SalesRead.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<SalesOrderProductReadModel> SalesOrderProductReadModels { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
