using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.UpdateStock
{
    public sealed record class UpdateStockCommand(Guid id,
        Guid productid, Guid warehouseId, int quantity) : IRequest<Result<Updated>>
    {
    }
}
