using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetEmployees
{
    public class GetEmployeeQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetEmployeeQuery, Result<List<EmployeeDto>>>
    {
        public async Task<Result<List<EmployeeDto>>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var cacheemployee = await cache.GetOrCreateAsync("employees", async entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var employees = await context.Employees.Select(e=>new EmployeeDto
                {
                    Department = e.Department,
                    JobTitle = e.JobTitle,
                    Salary = e.Salary,
                    HireDate = e.HireDate
                }).ToListAsync(cancellationToken);

                return employees;
            });
            return cacheemployee;
        }
    }
}
