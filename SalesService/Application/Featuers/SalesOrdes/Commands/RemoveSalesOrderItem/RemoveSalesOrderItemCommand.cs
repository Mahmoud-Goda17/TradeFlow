using MediatR;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.RemoveSalesOrderItem
{
    public sealed record RemoveSalesOrderItemCommand(Guid salesorderid, Guid productId): IRequest<Result<Updated>>
    {
    }
}
