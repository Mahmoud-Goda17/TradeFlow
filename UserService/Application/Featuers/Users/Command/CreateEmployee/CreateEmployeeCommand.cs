using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Command.CreateEmployee
{
    public sealed record class CreateEmployeeCommand(Guid userId,
        string department,
        string jobTitle, 
        decimal salary, 
        DateTimeOffset hireDate):IRequest<Result<EmployeeDto>>
    {
    }
}
