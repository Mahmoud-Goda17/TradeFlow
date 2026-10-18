using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.RemoveWhereHouse
{
    public sealed record RemoveWhereHouseCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
