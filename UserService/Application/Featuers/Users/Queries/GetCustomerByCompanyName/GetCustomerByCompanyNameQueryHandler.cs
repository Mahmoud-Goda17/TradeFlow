using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerByCompanyName
{
    public class GetCustomerByCompanyNameQueryHandler(IAppDbContext context) : IRequestHandler<GetCustomerByCompanyNameQuery, Result<List<CustomerDto>>>
    {
        public async Task<Result<List<CustomerDto>>> Handle(GetCustomerByCompanyNameQuery request, CancellationToken cancellationToken)
        {
            var name = request.companyName.Trim().ToLower();
            var customers = await context.Customers.Where(c => c.CompanyName.ToLower().Contains(name))
                .Select(c => new CustomerDto
                {
                    CompanyName = c.CompanyName,
                    TaxNumber = c.TaxNumber,
                }).ToListAsync(cancellationToken);
            return customers;

        }
    }
}
