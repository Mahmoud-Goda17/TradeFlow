using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.UpdateUser
{
    public class UpdateUserCommandHandler(IAppDbContext context,
        IMemoryCache cache, ILogger<UpdateUserCommandHandler> logger) :
        IRequestHandler<UpdateUserCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.id, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.id);
                return AppErrors.NotFound("User", request.id);
            }
            var email = request.email.Trim().ToLower();
            var updateResult = user.Update(request.fullName, email, request.phone, request.userType, request.isActive);
            if (updateResult.IsError)
            {
                logger.LogWarning("Failed to update user with id {Id}: {Errors}", request.id, updateResult.Errors);
                return updateResult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("users");
            return updateResult;
        }
    }
}
