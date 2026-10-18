using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.UpdateWhereHouse
{
    public sealed record UpdateWhereHouseCommand(Guid id, string name,string code, string addres): IRequest<Result<Updated>>
    {
    }
}
