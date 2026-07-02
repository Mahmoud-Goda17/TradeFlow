using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerById
{
    public class GetCustomerByIdHandler(IAppDbContext context,
        ILogger<GetCustomerByIdHandler> logger) : IRequestHandler<GetCutomerByIdQuery, Result<CustomerDto>>
    {
        public async Task<Result<CustomerDto>> Handle(GetCutomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await context.Customers.Where(c => c.Id == request.Id)
                .Select(c => new CustomerDto
                {
                    CompanyName = c.CompanyName,
                    TaxNumber = c.TaxNumber
                }).FirstOrDefaultAsync(cancellationToken);
            if (customer == null)
            {
                logger.LogWarning("Customer with id {Id} not found", request.Id);
                return AppErrors.NotFound("Custommer", request.Id);
            }
            return customer;
        }
    }
}
