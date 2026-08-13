using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.UpdateWhereHouse
{
    public sealed record class UpdateWhereHouseCommand(Guid id, string name,
        string code, string addres): IRequest<Result<Updated>>
    {
    }
}
