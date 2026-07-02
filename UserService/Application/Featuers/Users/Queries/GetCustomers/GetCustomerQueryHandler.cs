using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetCustomers
{
    public class GetCustomerQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetCustomerQuery, Result<List<CustomerDto>>>
    {
        public async Task<Result<List<CustomerDto>>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var customercache = await cache.GetOrCreateAsync("customers", async entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var customers = await context.Customers.AsNoTracking()
                .Select(c => new CustomerDto
                {
                    CompanyName = c.CompanyName,
                    TaxNumber = c.TaxNumber
                }).ToListAsync(cancellationToken);
                return customers;
            });
            return customercache;
        }
    }

}
