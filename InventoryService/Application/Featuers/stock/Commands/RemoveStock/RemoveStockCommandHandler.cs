using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.RemoveStock
{
    public class RemoveStockCommandHandler(IAppDbContext context,
        ILogger<RemoveStockCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveStockCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveStockCommand request, CancellationToken cancellationToken)
        {
            var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Id == request.id);
            if (stock is null)
            {
                logger.LogWarning("Stock with id {Id} not found.", request.id);
                return AppError.NotFound("stock", request.id.ToString());  
            }
            context.Stocks.Remove(stock);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("stocks");
            return Result.Deleted;
        }
    }
}
