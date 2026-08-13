using InventoryService.Application.Featuers.Wherehouse.Dto;
using InventoryService.Application.Featuers.Wherehouse.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseByCode
{
    public class GetWhereHouseByCodeQueryHandler(IAppDbContext context
        ) : IRequestHandler<GetWhereHouseByCodeQuery, Result<List<WherehouseDto>>>
    {
        public async Task<Result<List<WherehouseDto>>> Handle(GetWhereHouseByCodeQuery request, CancellationToken cancellationToken)
        {
            var code = request.code.Trim();
            var wherehouses = await context.Whouses.Where(p => p.Code.Contains(code))
                .Select(WerehouseMapper.ToDto)
                .ToListAsync(cancellationToken);
            return wherehouses;
        }
    }
}
