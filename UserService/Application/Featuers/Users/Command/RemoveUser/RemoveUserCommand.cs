using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveUser
{
    public sealed record class RemoveUserCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
