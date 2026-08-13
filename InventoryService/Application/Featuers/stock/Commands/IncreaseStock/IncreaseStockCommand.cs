using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.IncreaseStock
{
    public sealed record class IncreaseStockCommand(Guid id,int quantity) : IRequest<Result<Updated>>
    {
    }
}
