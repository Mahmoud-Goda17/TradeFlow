using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.ReleasesStock
{
    public class ReleaseStockCommandHandler(IAppDbContext context,
        ILogger<ReleaseStockCommandHandler> logger, IMemoryCache cache) : IRequestHandler<ReleaseStockCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(ReleaseStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Id == request.id, cancellationToken);
            if (stock is null)
            {
                logger.LogWarning("Stock with id {Id} not found", request.id);
                return AppError.NotFound("Stock", request.id.ToString());
            }
            var result = stock.Release(request.quantity);
            if (result.IsError)
            {
                logger.LogWarning("Failed to Release stock with id {Id}. Error: {Error}", request.id, result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("stocks");
            return result;
        }
    }
}
