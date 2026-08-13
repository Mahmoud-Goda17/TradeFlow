using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.RemoveWhereHouse
{
    public sealed record class RemoveWhereHouseCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
