using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.ReleasesStock
{
    public sealed record ReleaseStockCommand(Guid id, int quantity) : IRequest<Result<Updated>>
    {
    }
}
