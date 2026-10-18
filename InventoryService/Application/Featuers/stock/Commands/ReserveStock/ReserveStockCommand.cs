using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.ReserveStock
{
    public sealed record ReserveStockCommand(Guid id, int quantity) : IRequest<Result<Updated>>
    {
    }
}
