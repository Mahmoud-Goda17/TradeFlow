using InventoryService.Application.Featuers.stock.Dto;
using InventoryService.Application.Featuers.stock.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Queries.GetStockByProductId
{
    public class GetStockByProductIdQueryHandler(IAppDbContext context,
        ILogger<GetStockByProductIdQueryHandler> logger) : IRequestHandler<GetStockByProductIdQuery, Result<StockDto>>
    {
        public async Task<Result<StockDto>> Handle(GetStockByProductIdQuery request, CancellationToken cancellationToken)
        {
            var stock = await context.Stocks.
                 Where(x => x.ProductId == request.productid)
                .Select(StockMapper.ToDto)
                .FirstOrDefaultAsync(cancellationToken);
            if (stock is null)
            {
                logger.LogWarning("Stock not found for productId: {ProductId}", request.productid);
                return AppError.NotFoundByProductId("Stock ", request.productid.ToString());
            }
            return stock;
        }
    }
}
