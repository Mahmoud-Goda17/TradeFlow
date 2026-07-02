using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetEmployeeByUserId
{
    public class GetEmployeeByUserIdQueryHandler(IAppDbContext context,
        ILogger<GetEmployeeByUserIdQueryHandler> logger) : IRequestHandler<GetEmployeeByUserIdQuery, Result<EmployeeDto>>
    {
        public async Task<Result<EmployeeDto>> Handle(GetEmployeeByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.AnyAsync(u => u.Id == request.userid);
            if (!user)
            {
                logger.LogWarning("User with id {Id} not found", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var employee = await context.Employees.Where(e => e.UserId == request.userid)
                .Select(e => new EmployeeDto
                {
                    Department=e.Department,
                    HireDate=e.HireDate,
                    JobTitle=e.JobTitle,
                    Salary=e.Salary
                }).FirstOrDefaultAsync(cancellationToken);
            if (employee is null)
            {
                logger.LogWarning("Employee with user id {Id} not found", request.userid);
                return AppErrors.NotFound("Employee", request.userid);
            }
            return employee;
        }
    }
}
