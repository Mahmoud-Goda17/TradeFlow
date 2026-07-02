using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.RemoveEmployee
{
    public class RemoveEmployeeCommandHandler(IAppDbContext context,
        ILogger<RemoveEmployeeCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveEmployeeCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee= await context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (employee == null)
            {
                logger.LogWarning("Employee with id {Id} not found.", request.Id);
                return AppErrors.NotFound("Employee", request.Id);
            }
            context.Employees.Remove(employee);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("employees");
            return Result.Deleted;
        }
    }
}
