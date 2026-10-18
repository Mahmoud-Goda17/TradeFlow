using MediatR;
using SalesService.Application.Featuers.SalesOrdes.Dtos;
using Shared.Results;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderById
{
    public sealed record GetSalesOrderByIdQuery(Guid id): IRequest<Result<SalesOrderDto>>
    {
    }
}
