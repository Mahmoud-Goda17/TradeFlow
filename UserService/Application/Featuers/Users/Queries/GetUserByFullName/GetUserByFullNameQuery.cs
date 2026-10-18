using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetUserByFullName
{
    public sealed record  GetUserByFullNameQuery(string fullname) : IRequest<Result<List<UserDto>>>
    {
    }
}
