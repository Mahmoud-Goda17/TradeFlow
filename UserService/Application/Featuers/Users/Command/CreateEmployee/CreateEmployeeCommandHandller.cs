using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Featuers.Users.Mapper;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.CreateEmployee
{
    public class CreateEmployeeCommandHandller(IAppDbContext context,
        ILogger<CreateEmployeeCommandHandller> logger, IMemoryCache cache) : IRequestHandler<CreateEmployeeCommand, Result<EmployeeDto>>
    {
        public async Task<Result<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userId, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userId);
                return AppErrors.NotFound("User", request.userId);
            }
            var employeeResult = user.AddEmployee(Guid.NewGuid(), request.department, request.jobTitle, request.salary, request.hireDate);
            if(employeeResult.IsError)
            {
                logger.LogWarning("Failed to add employee for user with id {Id}: {Errors}", request.userId, employeeResult.Errors);
                return employeeResult.Errors;
            }
            context.Employees.Add(employeeResult.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("employees");
            return employeeResult.Value.Todto();
        }
    } 
}

