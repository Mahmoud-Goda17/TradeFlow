using InventoryService.Application.Featuers.stock.Dto;
using InventoryService.Application.Featuers.stock.Mapper;
using InventoryService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Queries.GetStocksByWhereHouseId
{
    public class GetStocksByWhereHouseIdQueryHandler(IAppDbContext context
        ) : IRequestHandler<GetStocksByWhereHouseIdQuery, Result<List<StockDto>>>
    {
        public async Task<Result<List<StockDto>>> Handle(GetStocksByWhereHouseIdQuery request, CancellationToken cancellationToken)
        {
            var stocks = await  context.Stocks
                .Where(s => s.WarehouseId == request.wherhouseid)
                .Select(StockMapper.ToDto)
                .ToListAsync(cancellationToken);
            return stocks;
        }
    }
}
