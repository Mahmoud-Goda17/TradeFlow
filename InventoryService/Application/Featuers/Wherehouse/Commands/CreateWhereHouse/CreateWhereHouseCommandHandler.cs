using InventoryService.Application.Featuers.Wherehouse.Dto;
using InventoryService.Application.Featuers.Wherehouse.Mapper;
using InventoryService.Application.Interfaces;
using InventoryService.Domain.Warehouse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.CreateWhereHouse
{
    public class CreateWhereHouseCommandHandler(IAppDbContext context,
        ILogger<CreateWhereHouseCommandHandler> logger, IMemoryCache cache) : IRequestHandler<CreateWhereHouseCommand, Result<WherehouseDto>>
    {
        public async Task<Result<WherehouseDto>> Handle(CreateWhereHouseCommand request, CancellationToken cancellationToken)
        {
            var wherehouse = await context.Whouses.AnyAsync(w => w.Code == request.code);
            if (wherehouse)
            {
                logger.LogWarning("Wherehouse with Code {Code} already exists.", request.code);
                return AppError.IsExcist("wherehouse", "Code", request.code);
            }
            var newWherehouse = WhereHouse.Create(Guid.NewGuid(),
                request.name, request.code, request.address, request.isActive);
            if (newWherehouse.IsError)
            {
                logger.LogWarning("Failed to create wherehouse: {Errors}", newWherehouse.Errors);
                return newWherehouse.Errors;
            }
            context.Whouses.Add(newWherehouse.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("wherehouses");
            return newWherehouse.Value.ToWherehouseDto();
        }
    }
}
