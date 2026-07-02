using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetEmployeeById
{
    public sealed record class GetEmployeeByIdQuery(Guid Id) : IRequest<Result<EmployeeDto>>
    {
    }
}
