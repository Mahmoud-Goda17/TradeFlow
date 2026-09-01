using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Featuers.SalesOrdes.Dtos;
using SalesService.Application.Featuers.SalesOrdes.Mapper;
using SalesService.Application.Interfaces;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrders
{
    public class GetSalesOrdersQueryHandler(IAppDbContext context) : IRequestHandler<GetSalesOrdersQuery, Result<List<SalesOrderDto>>>
    {
        public  async Task<Result<List<SalesOrderDto>>> Handle(GetSalesOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = context.Sales.AsQueryable();
            if (request.CustomerId.HasValue)
            {
                query = query.Where(x => x.CustomerId == request.CustomerId.Value);
            }
            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }
            if (!string.IsNullOrEmpty(request.OrderNumber))
            {
                query = query.Where(x => x.OrderNumber == request.OrderNumber);
            }
            var salesOrders = await query.Select(SalesOrderMapper.ToDto).ToListAsync(cancellationToken);
            return salesOrders;
        }
    }
}
