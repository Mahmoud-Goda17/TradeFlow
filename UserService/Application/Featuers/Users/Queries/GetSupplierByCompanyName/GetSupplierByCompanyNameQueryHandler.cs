using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetSupplierByCompanyName
{
    public class GetSupplierByCompanyNameQueryHandler(IAppDbContext context) : IRequestHandler<GetSupplierByCompanyNameQuery, Result<List<SupplierDto>>>
    {
        public async Task<Result<List<SupplierDto>>> Handle(GetSupplierByCompanyNameQuery request, CancellationToken cancellationToken)
        {
            var name= request.companyname.Trim().ToLower();
            var suppliers= await context.Suppliers.Where(s=>s.CompanyName.ToLower().Contains(name))
                .Select(s=>new SupplierDto
                {
                    CompanyName=s.CompanyName,
                    BankAccount=s.BankAccount,
                    TaxNumber=s.TaxNumber,
                }).ToListAsync(cancellationToken);
            return suppliers;
        }
    }
}
