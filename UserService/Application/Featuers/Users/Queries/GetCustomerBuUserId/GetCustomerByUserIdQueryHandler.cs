using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerBuUserId
{
    public class GetCustomerByUserIdQueryHandler(IAppDbContext context,
        ILogger<GetCustomerByUserIdQueryHandler> logger) : IRequestHandler<GetCustomerByUserIdQuery, Result<CustomerDto>>
    {
        public async Task<Result<CustomerDto>> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.AnyAsync(u => u.Id == request.userid, cancellationToken);
            if (!user)
            {
                logger.LogWarning("User with id {Id} not found", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var customer = await context.Customers.Where(c => c.UserId == request.userid)
                .Select(c => new CustomerDto
                {
                    CompanyName = c.CompanyName,
                    TaxNumber = c.TaxNumber,

                }).FirstOrDefaultAsync();
            if (customer is null)
            {
                logger.LogWarning("Customer with user id {Id} not found", request.userid);
                return AppErrors.NotFound("Customer", request.userid);
            }
            return customer;
        }
    }
}
