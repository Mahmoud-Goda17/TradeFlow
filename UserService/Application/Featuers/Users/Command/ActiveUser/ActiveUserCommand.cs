using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.ActiveUser
{
    public sealed record ActiveUserCommand(Guid userid) : IRequest<Result<Updated>>
    {
    }
}
