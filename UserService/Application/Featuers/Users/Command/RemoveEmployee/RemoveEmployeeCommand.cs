using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.RemoveEmployee
{
    public sealed record class RemoveEmployeeCommand(Guid Id) : IRequest<Result<Deleted>>
    {
    }
}
