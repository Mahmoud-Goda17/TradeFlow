using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.DecreaseStock
{
    public sealed record DecreaseStockCommand(Guid id,int quantity) : IRequest<Result<Updated>>
    {
    }
}
