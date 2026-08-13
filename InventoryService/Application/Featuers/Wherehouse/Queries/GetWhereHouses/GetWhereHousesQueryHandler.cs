using InventoryService.Application.Featuers.Wherehouse.Dto;
using InventoryService.Application.Featuers.Wherehouse.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouses
{
    public class GetWhereHousesQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetWhereHousesQuery, Result<List<WherehouseDto>>>
    {
        public async Task<Result<List<WherehouseDto>>> Handle(GetWhereHousesQuery request, CancellationToken cancellationToken)
        {
            var warehousesCache = await cache.GetOrCreateAsync("warehouses", entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var warehouses = context.Whouses.Select(WerehouseMapper.ToDto)
                    .ToListAsync(cancellationToken);
                      return warehouses;
                 });
            return warehousesCache;
        }
    }
}
