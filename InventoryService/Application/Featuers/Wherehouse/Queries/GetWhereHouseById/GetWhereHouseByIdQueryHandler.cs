using InventoryService.Application.Featuers.Wherehouse.Dto;
using InventoryService.Application.Featuers.Wherehouse.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseById
{
    public class GetWhereHouseByIdQueryHandler(IAppDbContext context,
        ILogger<GetWhereHouseByIdQueryHandler> logger) : IRequestHandler<GetWhereHouseByIdQuery, Result<WherehouseDto>>
    {
        public async Task<Result<WherehouseDto>> Handle(GetWhereHouseByIdQuery request, CancellationToken cancellationToken)
        {
            var wherehouse = await context.Whouses
                .Where(w => w.Id == request.id)
                .Select(WerehouseMapper.ToDto)
                .FirstOrDefaultAsync();
            if (wherehouse == null)
            {
                logger.LogWarning("Wherehouse with id {Id} not found", request.id);
                return AppError.NotFound("Wherehouse", request.id.ToString());
            }
            return wherehouse;
        }
    }
}
