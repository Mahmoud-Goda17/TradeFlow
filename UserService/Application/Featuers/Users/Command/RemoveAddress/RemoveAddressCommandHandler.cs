using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.RemoveAddress
{
    public class RemoveAddressCommandHandler(IAppDbContext context,
        ILogger<RemoveAddressCommandHandler> logger) : IRequestHandler<RemoveAddressCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
        {
            var user =  await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var removeresult = user.DeleteAddress();
            if(removeresult.IsError)
            {
                logger.LogWarning("Failed to remove address for user with id {Id}: {Errors}", request.userid, removeresult.Errors);
                return removeresult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            return removeresult.Value;

        }
    }
}
