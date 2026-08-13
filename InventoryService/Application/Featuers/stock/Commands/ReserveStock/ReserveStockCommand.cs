using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.ReserveStock
{
    public sealed record class ReserveStockCommand(Guid id, int quantity) : IRequest<Result<Updated>>
    {
    }
}
