using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.RemoveUser
{
    public class RemoveUserCommandHandler(IAppDbContext context,
        ILogger<RemoveUserCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveUserCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.id);
                return AppErrors.NotFound("User", request.id);
            }
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("users");
            return Result.Deleted;
        }
    }
}
