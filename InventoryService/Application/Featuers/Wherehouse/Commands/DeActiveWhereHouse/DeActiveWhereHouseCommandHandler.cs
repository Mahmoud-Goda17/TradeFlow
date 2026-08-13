using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.DeActiveWhereHouse
{
    public class DeActiveWhereHouseCommandHandler(IAppDbContext context,
        ILogger<DeActiveWhereHouseCommandHandler> logger, IMemoryCache cache) : IRequestHandler<DeActiveWhereHouseCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(DeActiveWhereHouseCommand request, CancellationToken cancellationToken)
        {
            var existingWhereHouse = await context.Whouses.FirstOrDefaultAsync(p => p.Id == request.id);
            if (existingWhereHouse == null)
            {
                logger.LogWarning("WhereHouse with id {Id} not found.", request.id);
                return AppError.NotFound("WhereHouse", request.id.ToString());
            }
            var result = existingWhereHouse.Deactivate();
            if (result.IsError)
            {
                logger.LogWarning("Failed to Deactivate WhereHouse with id {Id}: {Errors}", request.id, result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("wherehouses");
            return result;
        }
    }
}
