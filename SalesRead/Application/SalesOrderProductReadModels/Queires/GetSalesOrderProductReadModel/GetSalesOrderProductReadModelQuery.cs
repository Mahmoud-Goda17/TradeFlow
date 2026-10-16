using MediatR;
using SalesRead.Application.SalesOrderProductReadModels.Dtos;
using Shared.Results;

namespace SalesRead.Application.SalesOrderProductReadModels.Queires.GetSalesOrderProductReadModel
{
    public sealed record class GetSalesOrderProductReadModelQuery:IRequest<Result<List<SalesOrderProductReadModelDto>>>
    {
    }
}
