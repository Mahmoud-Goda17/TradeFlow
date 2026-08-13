using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.UpdateWhereHouse
{
    public class UpdateWhereHouseCommandHandler(IAppDbContext context,
        ILogger<UpdateWhereHouseCommandHandler> logger, IMemoryCache cache) : IRequestHandler<UpdateWhereHouseCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateWhereHouseCommand request, CancellationToken cancellationToken)
        {
            var existingWhereHouse = await context.Whouses.FirstOrDefaultAsync(p => p.Id == request.id);
            if (existingWhereHouse == null)
            {
                logger.LogWarning("Wherehouse with id {Id} not found.", request.id);
                return AppError.NotFound("wherehouse", request.id.ToString());
            }
            var exists = context.Whouses.Any(p => p.Id != request.id && p.Code == request.code);
            if (exists)
            {
                logger.LogWarning("Wherehouse with code {Code} already exists.", request.code);
                return AppError.IsExcist("wherehouse", "code", request.code);
            }
            var updateresult = existingWhereHouse.Update(request.name, request.code, request.addres);
            if (updateresult.IsError)
            {
                logger.LogWarning("Failed to update wherehouse with id {Id}: {Errors}", request.id, updateresult.Errors);
                return updateresult.Errors;
            }
           await  context.SaveChangesAsync(cancellationToken);
            cache.Remove("wherehouses");
            return updateresult;
        }
    }
}
