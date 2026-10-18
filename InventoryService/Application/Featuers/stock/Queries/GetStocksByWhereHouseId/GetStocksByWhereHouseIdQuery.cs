using InventoryService.Application.Featuers.stock.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Queries.GetStocksByWhereHouseId
{
    public sealed record GetStocksByWhereHouseIdQuery(Guid wherhouseid): IRequest<Result<List<StockDto>>>
    {
    }
}
