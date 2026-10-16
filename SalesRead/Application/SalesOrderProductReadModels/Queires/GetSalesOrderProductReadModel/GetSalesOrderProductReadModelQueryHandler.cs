using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SalesRead.Application.Interfaces;
using SalesRead.Application.SalesOrderProductReadModels.Dtos;
using Shared.Results;

namespace SalesRead.Application.SalesOrderProductReadModels.Queires.GetSalesOrderProductReadModel
{
    public class GetSalesOrderProductReadModelQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetSalesOrderProductReadModelQuery, Result<List<SalesOrderProductReadModelDto>>>
    {
        public async Task<Result<List<SalesOrderProductReadModelDto>>> Handle(GetSalesOrderProductReadModelQuery request, CancellationToken cancellationToken)
        {
            var salesOrderProductReadModelsCache = await cache.GetOrCreateAsync("salesOrderProductReadModels", async entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var salesOrderProductReadModels = await context.SalesOrderProductReadModels.Select(s => new SalesOrderProductReadModelDto
                {
                    SalesOrderId = s.SalesOrderId,
                    OrderNumber = s.OrderNumber,
                    ProductId = s.ProductId,
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                    UnitPrice = s.UnitPrice,
                    Discount = s.Discount,
                    Tax = s.Tax,
                    Total = s.Total
                }).ToListAsync(cancellationToken);
                return salesOrderProductReadModels;
            });
            return salesOrderProductReadModelsCache;
        }
    }
}
