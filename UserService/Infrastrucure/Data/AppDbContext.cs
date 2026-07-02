using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Addresses;
using UserService.Domain.Customers;
using UserService.Domain.Employess;
using UserService.Domain.Supliers;
using UserService.Domain.Users;

namespace UserService.Infrastrucure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();

        public DbSet<Empioyee> Employees => Set<Empioyee>();

        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Supplier> Suppliers => Set<Supplier>();

        public DbSet<Address> Addresses => Set<Address>();

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
