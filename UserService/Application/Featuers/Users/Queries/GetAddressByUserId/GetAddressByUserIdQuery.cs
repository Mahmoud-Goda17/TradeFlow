using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetAddressByUserId
{
    public sealed record class GetAddressByUserIdQuery(Guid userid) : IRequest<Result<List<AddressDto>>>
    {
    }
}
