using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetSupliersById
{
    public sealed record class GetSuplierByIdQuery(Guid Id) : IRequest<Result<SupplierDto>>
    {
    }
}
