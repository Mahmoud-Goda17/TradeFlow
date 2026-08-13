using InventoryService.Application.Featuers.stock.Dto;
using InventoryService.Application.Featuers.stock.Interfaces;
using InventoryService.Application.Featuers.stock.Mapper;
using InventoryService.Application.Interfaces;
using InventoryService.Domain.Stock;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.CreateStock
{
    public class CreateStockCommandHandler(IAppDbContext context,
        ILogger<CreateStockCommandHandler> logger, IMemoryCache cache,IProductServiceClient productService) : IRequestHandler<CreateStockCommand, Result<StockDto>>
    {
        public async Task<Result<StockDto>> Handle(CreateStockCommand request, CancellationToken cancellationToken)
        {
            var productExists = await productService.GetProductByIdAsync(request.productId, cancellationToken);
            if (productExists == null)
            {
                logger.LogWarning("Product with ID {ProductId} does not exist.", request.productId);
                return AppError.NotFound("product", request.productId.ToString());
            }

            var warehouseExists = await context.Whouses.AnyAsync(w => w.Id == request.warehouseId,cancellationToken);
            if (!warehouseExists)
            {
                logger.LogWarning("Warehouse with ID {WarehouseId} does not exist.", request.warehouseId);
                return AppError.NotFound("warehouse", request.warehouseId.ToString());
            }

            var stockExists = await context.Stocks.AnyAsync(s => s.ProductId == request.productId && s.WarehouseId == request.warehouseId,cancellationToken);
            if (stockExists)
            {
                logger.LogWarning("Stock for product {ProductId} in warehouse {WarehouseId} already exists.", request.productId, request.warehouseId);
                return AppError.IsExcist("stock", "productId", "warehouseId", request.productId.ToString(), request.warehouseId.ToString());
            }
            var newStock = Stock.Create(Guid.NewGuid(), request.productId, request.warehouseId, request.quantity);
            if (newStock.IsError)
            {
                logger.LogWarning("Failed to create stock: {Errors}", newStock.Errors);
                return newStock.Errors;
            }
            context.Stocks.Add(newStock.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("stocks");
            return newStock.Value.ToStockDto();
        }
    }
}
