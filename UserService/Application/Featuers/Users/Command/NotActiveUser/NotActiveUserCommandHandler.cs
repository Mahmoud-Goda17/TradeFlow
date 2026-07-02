using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Command.RemoveUser;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.NotActiveUser
{
    public class NotActiveUserCommandHandler(IAppDbContext context,
        ILogger<RemoveUserCommandHandler> logger, IMemoryCache cache) : IRequestHandler<NotActiveUserCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(NotActiveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid,cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var result = user.BeNotActive();
            if (result.IsError)
            {
                logger.LogWarning("Failed to not active user with id {Id}: {Errors}", request.userid, result.Errors);
                return result.Errors;
            }
           await context.SaveChangesAsync(cancellationToken);
            cache.Remove("users");
            return result;
        }
    }
}
