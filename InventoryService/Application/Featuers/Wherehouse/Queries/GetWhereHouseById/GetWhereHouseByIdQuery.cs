using InventoryService.Application.Featuers.Wherehouse.Dto;
using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseById
{
    public sealed record GetWhereHouseByIdQuery(Guid id) : IRequest<Result<WherehouseDto>>
    {
    }
}
