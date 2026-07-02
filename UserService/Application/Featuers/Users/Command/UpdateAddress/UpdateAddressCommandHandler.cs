using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.UpdateAddress
{
    public class UpdateAddressCommandHandler(IAppDbContext context,
        ILogger<UpdateAddressCommandHandler> logger) : IRequestHandler<UpdateAddressCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var updateresult= user.UpdateAdreeses(request.Addresses);
            if(updateresult.IsError)
            {
                logger.LogWarning("faild to update Address with id : {Id} : {Errors}",request.userid,updateresult.Errors);
                return updateresult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            return updateresult.Value;
        }
    }
}
