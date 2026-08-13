using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.RemoveWhereHouse
{
    public class RemoveWhereHouseCommandHandler(IAppDbContext context,
        ILogger<RemoveWhereHouseCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveWhereHouseCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveWhereHouseCommand request, CancellationToken cancellationToken)
        {
            var existingWhereHouse = await context.Whouses.FirstOrDefaultAsync(p => p.Id == request.id);
            if (existingWhereHouse == null)
            {
                logger.LogWarning("WhereHouse with id {Id} not found.", request.id);
                return AppError.NotFound("wherehouse", request.id.ToString());
            }
            context.Whouses.Remove(existingWhereHouse);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("wherehouses");
            return Result.Deleted;
        }
    }
}
