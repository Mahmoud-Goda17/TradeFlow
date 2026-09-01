using MediatR;
using SalesService.Application.Featuers.SalesOrdes.Dtos;
using SalesService.Domain.SalesOrders;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrders
{
    public sealed record class GetSalesOrdersQuery(Guid? CustomerId,
    OrderStatus? Status,
    string? OrderNumber) : IRequest<Result<List<SalesOrderDto>>>
    {
    }
}
