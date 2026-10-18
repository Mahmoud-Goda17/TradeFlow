using MediatR;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemDiscount
{
    public sealed record UpdateSalesOrderItemDiscountCommand(Guid salesorderid, Guid productId, decimal newDiscount) : IRequest<Result<Updated>>
    {
    }
}
