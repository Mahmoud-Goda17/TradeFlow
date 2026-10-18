using InventoryService.Application.Featuers.stock.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Queries.GetStockByProductId
{
    public sealed record GetStockByProductIdQuery(Guid productid): IRequest<Result<StockDto>>
    {
    }
}
