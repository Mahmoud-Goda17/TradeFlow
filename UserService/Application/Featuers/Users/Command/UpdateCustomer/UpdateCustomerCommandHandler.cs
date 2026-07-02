using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(IAppDbContext context,
        ILogger<UpdateCustomerCommandHandler> logger, IMemoryCache cache) : IRequestHandler<UpdateCustomerCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.usreid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.usreid);
                return AppErrors.NotFound("User", request.usreid);
            }
            var updateresult= user.UpdateCustomer(request.companyName, request.taxNumber);
            if (updateresult.IsError)
            {
                logger.LogWarning("Failed to update customer for user with id {Id}: {Errors}", request.usreid, updateresult.Errors);
                return updateresult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("customers");
            return updateresult;

        }
    }
}
