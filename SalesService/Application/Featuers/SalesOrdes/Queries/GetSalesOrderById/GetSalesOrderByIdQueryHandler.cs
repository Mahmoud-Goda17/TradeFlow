using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesService.Application.Featuers.SalesOrdes.Dtos;
using SalesService.Application.Featuers.SalesOrdes.Mapper;
using SalesService.Application.Interfaces;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderById
{
    public class GetSalesOrderByIdQueryHandler(IAppDbContext context,
        ILogger<GetSalesOrderByIdQueryHandler> logger) : IRequestHandler<GetSalesOrderByIdQuery, Result<SalesOrderDto>>
    {
        public async Task<Result<SalesOrderDto>> Handle(GetSalesOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var salesOrder = await context.Sales.Where(x => x.Id == request.id)
                .Select(SalesOrderMapper.ToDto)
                .FirstOrDefaultAsync(cancellationToken);
            if (salesOrder == null)
            {
                logger.LogWarning("SalesOrder with id {id} not found", request.id);
                return AppErrors.NotFound("SalesOrder", request.id.ToString());
            }
            return salesOrder;
        }
    }
}
