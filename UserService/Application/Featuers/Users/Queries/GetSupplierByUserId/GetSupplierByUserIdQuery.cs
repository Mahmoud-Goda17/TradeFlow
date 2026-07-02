using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetSupplierByUserId
{
    public sealed record class GetSupplierByUserIdQuery(Guid userid) : IRequest<Result<SupplierDto>>
    {
    }
}
