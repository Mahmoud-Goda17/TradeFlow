using InventoryService.Application.Featuers.stock.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.CreateStock
{
    public sealed record class CreateStockCommand(Guid productId,
        Guid warehouseId, int quantity) : IRequest<Result<StockDto>>
    {
    }
}
