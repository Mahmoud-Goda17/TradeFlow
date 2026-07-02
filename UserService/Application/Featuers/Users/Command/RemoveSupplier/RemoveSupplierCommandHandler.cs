using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.RemoveSupplier
{
    public class RemoveSupplierCommandHandler(IAppDbContext context,
        ILogger<RemoveSupplierCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveSupplierCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await context.Suppliers.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (supplier == null)
            {
                logger.LogWarning("Supplier with id {Id} not found.", request.Id);
                return AppErrors.NotFound("Supplier", request.Id);
            }
            context.Suppliers.Remove(supplier);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("suppliers");
            return Result.Deleted;
        }
    }
}
