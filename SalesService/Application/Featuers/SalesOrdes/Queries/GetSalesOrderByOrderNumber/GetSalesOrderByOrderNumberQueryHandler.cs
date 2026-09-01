using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Featuers.SalesOrdes.Dtos;
using SalesService.Application.Featuers.SalesOrdes.Mapper;
using SalesService.Application.Interfaces;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderByOrderNumber
{
    public class GetSalesOrderByOrderNumberQueryHandler(IAppDbContext context,
       ILogger<GetSalesOrderByOrderNumberQueryHandler> logger) : IRequestHandler<GetSalesOrderByOrderNumberQuery, Result<SalesOrderDto>>
    {
        public  async Task<Result<SalesOrderDto>> Handle(GetSalesOrderByOrderNumberQuery request, CancellationToken cancellationToken)
        {
            var salesOrder = await context.Sales.Where(x => x.OrderNumber == request.OrderNumber)
                .Select(SalesOrderMapper.ToDto)
                .FirstOrDefaultAsync(cancellationToken);
            if (salesOrder == null)
            {
                logger.LogWarning("SalesOrder with OrderNumber {OrderNumber} not found", request.OrderNumber);
                return AppErrors.NotFound("SalesOrder", request.OrderNumber);
            }
            return salesOrder;
        }
    }
}
