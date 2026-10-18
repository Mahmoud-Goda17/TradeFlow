using MediatR;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;

namespace UserService.Application.Featuers.Users.Queries.GetEmployeeByUserId
{
    public sealed record GetEmployeeByUserIdQuery(Guid userid) : IRequest<Result<EmployeeDto>>
    {
    }
}
