using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetSuppliers
{
    public class GetSupliersQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetSupliersQuery, Result<List<SupplierDto>>>
    {
        public async Task<Result<List<SupplierDto>>> Handle(GetSupliersQuery request, CancellationToken cancellationToken)
        {
            var supplierscache = await cache.GetOrCreateAsync("suppliers", entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var suppliers = context.Suppliers.Select(s => new SupplierDto
                {
                    CompanyName = s.CompanyName,
                    TaxNumber = s.TaxNumber
                }).ToListAsync(cancellationToken);
                return suppliers;
            });
            return supplierscache;
        }
    }
}
