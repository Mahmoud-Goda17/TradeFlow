using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryHandler(IAppDbContext context,
        ILogger<GetEmployeeByIdQueryHandler> logger) : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
    {
        public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await context.Employees.Where(e => e.Id == request.Id)
                .Select(e => new EmployeeDto
                {
                    Department = e.Department,
                    JobTitle = e.JobTitle,
                    Salary = e.Salary,
                    HireDate = e.HireDate
                }).FirstOrDefaultAsync(cancellationToken);
            if (employee == null)
            {
                logger.LogWarning("Employee with id {Id} not found", request.Id);
                return AppErrors.NotFound("Employee", request.Id);
            }
            return employee;
        }
    }
}
