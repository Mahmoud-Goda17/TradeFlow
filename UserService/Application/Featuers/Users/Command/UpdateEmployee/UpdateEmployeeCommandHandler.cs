using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler(IAppDbContext context,
        ILogger<UpdateEmployeeCommandHandler> logger, IMemoryCache cache) : IRequestHandler<UpdateEmployeeCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var employeeresult = user.UpdateEmployee(request.department, request.jobTitle, request.salary, request.hireDate);
            if (employeeresult.IsError)
            {
                logger.LogWarning("Failed to update employee for user with id {Id}: {Errors}", request.userid, employeeresult.Errors);
                return employeeresult.Errors;
            }
            if (employeeresult.IsError)
            {
                logger.LogWarning("Failed to update employee for user with id {Id}: {Errors}", request.userid, employeeresult.Errors);
                return employeeresult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("employees");
            return employeeresult.Value;
        }
    }
}
