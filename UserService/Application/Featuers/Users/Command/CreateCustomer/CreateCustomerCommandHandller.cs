using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Featuers.Users.Mapper;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.CreateCustomer
{
    public class CreateCustomerCommandHandller(IAppDbContext context,
        ILogger<CreateCustomerCommandHandller> logger, IMemoryCache cache) : IRequestHandler<CreateCustomerCommand, Result<CustomerDto>>
    {
        public async Task<Result<CustomerDto>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.usreid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.usreid);
                return AppErrors.NotFound("User", request.usreid);
            }
            var customerResult = user.AddCustomer(Guid.NewGuid(), request.companyName, request.taxNumber);
            if (customerResult.IsError)
            {
                logger.LogWarning("Failed to add customer for user with id {Id}: {Errors}", request.usreid, customerResult.Errors);
                return customerResult.Errors;
            }
            context.Customers.Add(customerResult.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("customers");
            return customerResult.Value.ToDto();
        }
    }
}
