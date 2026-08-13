using InventoryService.Application.Featuers.Wherehouse.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.CreateWhereHouse
{
    public sealed record class CreateWhereHouseCommand(string name,
        string code, string address, bool isActive): IRequest<Result<WherehouseDto>>
    {
    }
}
