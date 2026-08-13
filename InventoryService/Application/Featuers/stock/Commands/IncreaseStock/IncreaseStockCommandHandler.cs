using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.IncreaseStock
{
    public class IncreaseStockCommandHandler(IAppDbContext context,
        ILogger<IncreaseStockCommandHandler> logger, IMemoryCache cache) : IRequestHandler<IncreaseStockCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(IncreaseStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Id == request.id,cancellationToken);
            if (stock is null)
            {
                logger.LogWarning("Stock with id {Id} not found", request.id);
                return AppError.NotFound("Stock", request.id.ToString());
            }
            var result = stock.Increase(request.quantity);
            if (result.IsError)
            {
                logger.LogWarning("Failed to increase stock with id {Id}. Error: {Error}", request.id, result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("stocks");
            return result;
        }
    }
}
