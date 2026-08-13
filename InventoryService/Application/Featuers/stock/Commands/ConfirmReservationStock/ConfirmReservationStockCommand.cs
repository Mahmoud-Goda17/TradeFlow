using MediatR;
using Shared.Results;

namespace InventoryService.Application.Featuers.stock.Commands.ConfirmReservationStock
{
    public sealed record class ConfirmReservationStockCommand(Guid id, int quantity) : IRequest<Result<Updated>>
    {
    }
}
