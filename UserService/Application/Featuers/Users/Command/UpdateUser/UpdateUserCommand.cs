using MediatR;
using Shared.Results;
using UserService.Domain.Users;

namespace UserService.Application.Featuers.Users.Command.UpdateUser
{
    public sealed record  UpdateUserCommand(Guid id,
        string fullName,
        string email,
        string phone,
        UserType userType,
        bool isActive) : IRequest<Result<Updated>>
    {
    }
}
