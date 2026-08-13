using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.ActiveWherehouse
{
    public sealed record class ActiveWherehouseCommand(Guid id) : IRequest<Result<Updated>>
    {
    }
}
