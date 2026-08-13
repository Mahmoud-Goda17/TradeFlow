using InventoryService.Application.Featuers.stock.Dto;
using InventoryService.Application.Featuers.stock.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Queries.GetStockById
{
    public class GetStockByIdQueryHandler(IAppDbContext context,
        ILogger<GetStockByIdQueryHandler> logger) : IRequestHandler<GetStockByIdQuery, Result<StockDto>>
    {
        public async Task<Result<StockDto>> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
        {
            var stock = await context.Stocks.Where(x => x.Id == request.id)
                .Select(StockMapper.ToDto)
                .FirstOrDefaultAsync(cancellationToken);
            if (stock is null)
            {
                logger.LogWarning("Stock with id {id} not found", request.id);
                return AppError.NotFound("Stock",request.id.ToString());
            }
            return stock;
        }
    }
}
