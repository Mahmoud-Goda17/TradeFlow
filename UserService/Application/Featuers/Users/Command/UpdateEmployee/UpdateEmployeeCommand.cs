using MediatR;
using Shared.Results;

namespace UserService.Application.Featuers.Users.Command.UpdateEmployee
{
    public sealed record class UpdateEmployeeCommand(Guid userid,
        string department,
        string jobTitle,
        decimal salary,
        DateTimeOffset hireDate):IRequest<Result<Updated>>
    {
    }
}
