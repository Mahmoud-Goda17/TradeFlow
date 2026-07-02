using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.RemoveCustomer
{
    public class RemoveCustomerCommandHandler(IAppDbContext context,
        ILogger<RemoveCustomerCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveCustomerCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (customer == null)
            {
                logger.LogWarning("Customer with id {Id} not found.", request.Id);
                return AppErrors.NotFound("Customer", request.Id);
            }
            context.Customers.Remove(customer);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("customers");
            return Result.Deleted;
        }
    }
}
