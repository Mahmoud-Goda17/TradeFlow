using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.DeActiveWhereHouse
{
    public sealed record  DeActiveWhereHouseCommand(Guid id) : IRequest<Result<Updated>>
    {
    }
}
