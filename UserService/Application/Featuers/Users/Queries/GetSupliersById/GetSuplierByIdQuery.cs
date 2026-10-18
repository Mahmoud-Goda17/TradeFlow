using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetSupliersById
{
    public sealed record GetSuplierByIdQuery(Guid Id) : IRequest<Result<SupplierDto>>
    {
    }
}
