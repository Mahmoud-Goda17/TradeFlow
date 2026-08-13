using InventoryService.Application.Featuers.stock.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Queries.GetStockById
{
    public sealed record class GetStockByIdQuery(Guid id):IRequest<Result<StockDto>>
    {
    }
}
