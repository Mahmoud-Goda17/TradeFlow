using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Domain.Users;

namespace UserService.Application.Featuers.Users.Command.CreateUser
{
    public sealed record  CreateUserCommand(string fullName,
        string email,
        string phone,
        UserType userType,
        bool isActive): IRequest<Result<UserDto>>
    {
    }
}
