using MediatR;
using SalesService.Application.Featuers.SalesOrdes.Dtos;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderByOrderNumber
{
    public sealed record class GetSalesOrderByOrderNumberQuery(string OrderNumber) : IRequest<Result<SalesOrderDto>>
    {
    }
}
