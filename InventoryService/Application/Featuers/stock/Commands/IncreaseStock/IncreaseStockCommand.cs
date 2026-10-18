using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.IncreaseStock
{
    public sealed record IncreaseStockCommand(Guid id,int quantity) : IRequest<Result<Updated>>
    {
    }
}
