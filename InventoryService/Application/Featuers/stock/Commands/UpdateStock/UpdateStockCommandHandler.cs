using InventoryService.Application.Featuers.stock.Interfaces;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.UpdateStock
{
    public class UpdateStockCommandHandler(IAppDbContext context,
        ILogger<UpdateStockCommandHandler> logger, IMemoryCache cache, IProductServiceClient productService) : IRequestHandler<UpdateStockCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var productExists = await productService.GetProductByIdAsync(request.productid, cancellationToken);
            if (productExists == null)
            {
                logger.LogWarning("Product with ID {ProductId} does not exist.", request.productid);
                return AppError.NotFound("product", request.productid.ToString());
            }

            var warehouseExists = await context.Whouses.AnyAsync(w => w.Id == request.warehouseId, cancellationToken);
            if (!warehouseExists)
            {
                logger.LogWarning("Warehouse with ID {WarehouseId} does not exist.", request.warehouseId);
                return AppError.NotFound("warehouse", request.warehouseId.ToString());
            }

            var stockExists = await context.Stocks.AnyAsync(
                s => s.ProductId == request.productid &&
                     s.WarehouseId == request.warehouseId &&
                     s.Id != request.id,
                cancellationToken);
            if (stockExists)
            {
                logger.LogWarning("Stock for product {ProductId} in warehouse {WarehouseId} already exists.", request.productid, request.warehouseId);
                return AppError.IsExcist("stock", "productId", "warehouseId", request.productid.ToString(), request.warehouseId.ToString());
            }

            var stock = await context.Stocks.FirstOrDefaultAsync(s => s.Id == request.id, cancellationToken);
            if (stock == null)
            {
                logger.LogWarning("Stock with ID {StockId} does not exist.", request.id);
                return AppError.NotFound("stock", request.id.ToString());
            }
            var updateResult = stock.Update(request.productid, request.warehouseId, request.quantity);

            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("stocks");
            return updateResult;
        }
    }
}
