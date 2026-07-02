using Microsoft.EntityFrameworkCore;
using UserService.Domain.Addresses;
using UserService.Domain.Customers;
using UserService.Domain.Employess;
using UserService.Domain.Supliers;
using UserService.Domain.Users;

namespace UserService.Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Empioyee> Employees { get; }
        public DbSet<Customer> Customers { get; }
        public DbSet<Supplier> Suppliers { get; }
        public DbSet<Address> Addresses { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
