using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.ActiveWherehouse
{
    public class ActiveWherehouseCommandHandler(IAppDbContext context,
        ILogger<ActiveWherehouseCommandHandler> logger, IMemoryCache cache) : IRequestHandler<ActiveWherehouseCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(ActiveWherehouseCommand request, CancellationToken cancellationToken)
        {
            var existingWherehouse = await context.Whouses.FirstOrDefaultAsync(p => p.Id == request.id);
            if (existingWherehouse == null)
            {
                logger.LogWarning("Wherehouse with id {Id} not found.", request.id);
                return AppError.NotFound("wherehouse", request.id.ToString());
            }
            var result = existingWherehouse.Activate();
            if (result.IsError)
            {
                logger.LogWarning("Failed to activate wherehouse with id {Id}: {Errors}", request.id, result.Errors);
                return result.Errors;
            }
           await context.SaveChangesAsync(cancellationToken);
            cache.Remove("wherehouses");
            return result;
        }
    }
}
