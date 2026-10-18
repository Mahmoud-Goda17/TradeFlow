using MediatR;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.AddItemToSalesOrder
{
    public sealed record AddItemToSalesOrderCommand(Guid salesorderid,Guid productId,int quantity,
        decimal unitPrice, decimal discount, decimal tax) : IRequest<Result<Updated>>
    {
    }
}
