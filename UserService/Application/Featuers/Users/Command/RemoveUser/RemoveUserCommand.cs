using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveUser
{
    public sealed record  RemoveUserCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
