using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.ReserveStock
{
    public class ReserveStockCommandHandler(IAppDbContext context,
        ILogger<ReserveStockCommandHandler> logger, IMemoryCache cache) : IRequestHandler<ReserveStockCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(ReserveStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (stock is null)
            {
                logger.LogWarning("Stock with id {Id} not found", request.id);
                return AppError.NotFound("Stock", request.id.ToString());
            }
            var result = stock.Reserve(request.quantity);
            if (result.IsError)
            {
                logger.LogWarning("Failed to Reserve stock with id {Id}. Error: {Error}", request.id, result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("stocks");
            return result;
        }
    }
}
