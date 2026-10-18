using InventoryService.Application.Featuers.Wherehouse.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseByCode
{
    public sealed record GetWhereHouseByCodeQuery(string code): IRequest<Result<List<WherehouseDto>>>
    {
    }
}
