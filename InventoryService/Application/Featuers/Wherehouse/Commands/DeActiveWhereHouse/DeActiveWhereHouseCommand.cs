using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.DeActiveWhereHouse
{
    public sealed record class DeActiveWhereHouseCommand(Guid id) : IRequest<Result<Updated>>
    {
    }
}
