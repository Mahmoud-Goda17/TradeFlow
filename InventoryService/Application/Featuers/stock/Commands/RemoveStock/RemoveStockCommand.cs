using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.RemoveStock
{
    public sealed record class RemoveStockCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
