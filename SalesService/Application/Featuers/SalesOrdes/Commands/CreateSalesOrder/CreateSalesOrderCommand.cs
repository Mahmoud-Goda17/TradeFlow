using MediatR;
using SalesService.Application.Featuers.SalesOrdes.Commands.AddItemToSalesOrder;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.CreateSalesOrder
{
    public sealed record CreateSalesOrderCommand(string orderNumber, Guid customerId, string shippingAddress, List<AddItemToSalesOrderCommand> items) : IRequest<Result<Guid>>
    {
    }
}
