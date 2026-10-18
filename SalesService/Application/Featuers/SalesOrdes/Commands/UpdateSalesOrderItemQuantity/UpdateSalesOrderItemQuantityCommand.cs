using MediatR;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemQuantity
{
    public sealed record UpdateSalesOrderItemQuantityCommand(Guid salesorderid, Guid productId, int newQuantity) : IRequest<Result<Updated>>
    {
    }
}
