using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetEmployees
{
    public sealed record  GetEmployeeQuery : IRequest<Result<List<EmployeeDto>>>
    {
    }
}
