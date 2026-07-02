using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.NotActiveUser
{
    public sealed record class NotActiveUserCommand(Guid userid) : IRequest<Result<Updated>>
    {
    }
}
