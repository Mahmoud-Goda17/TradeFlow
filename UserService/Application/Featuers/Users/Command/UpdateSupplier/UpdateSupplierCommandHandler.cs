using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.UpdateSupplier
{
    public class UpdateSupplierCommandHandler(IAppDbContext context,
        ILogger<UpdateSupplierCommandHandler> logger, IMemoryCache cache) : IRequestHandler<UpdateSupplierCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var supplier = user.UpdateSupplier(request.companyName, request.taxNumber,request.bankAccount);
            if (supplier.IsError)
            {
                logger.LogWarning("Failed to update supplier for user with id {Id}: {Errors}", request.userid, supplier.Errors);
                return supplier.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("suppliers");
            return supplier.Value;
        }
    }
}
