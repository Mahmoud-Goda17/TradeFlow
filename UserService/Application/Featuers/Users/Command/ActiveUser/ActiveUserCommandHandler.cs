using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Command.RemoveUser;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.ActiveUser
{
    public class ActiveUserCommandHandler(IAppDbContext context,
        ILogger<RemoveUserCommandHandler> logger, IMemoryCache cache) : IRequestHandler<ActiveUserCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(ActiveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid,cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var result = user.BeActive();
            if (result.IsError)
            {
                logger.LogWarning("Failed to active user with id {Id}: {Errors}", request.userid, result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("users");
            return result;
        }
    }
}
