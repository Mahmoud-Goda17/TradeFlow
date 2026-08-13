using InventoryService.Application.Featuers.Wherehouse.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouses
{
    public sealed record class GetWhereHousesQuery : IRequest<Result<List<WherehouseDto>>>
    {
    }
}
