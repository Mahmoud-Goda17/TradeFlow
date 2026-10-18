using InventoryService.Application.Featuers.Wherehouse.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.SearchByName
{
    public sealed record SearchByNameQuery(string name) : IRequest<Result<List<WherehouseDto>>>
    {
    }
}
