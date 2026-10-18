using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.RemoveStock
{
    public sealed record  RemoveStockCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
