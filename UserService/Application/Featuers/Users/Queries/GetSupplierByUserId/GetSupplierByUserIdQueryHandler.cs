using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetSupplierByUserId
{
    public class GetSupplierByUserIdQueryHandler(IAppDbContext context,
        ILogger<GetSupplierByUserIdQueryHandler> logger) : IRequestHandler<GetSupplierByUserIdQuery, Result<SupplierDto>>
    {
        public async Task<Result<SupplierDto>> Handle(GetSupplierByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.AnyAsync(u => u.Id == request.userid);
            if (!user)
            {
                logger.LogWarning("User with id {Id} not found", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var supplier = await context.Suppliers.Where(s => s.UserId == request.userid)
                .Select(s => new SupplierDto
                {
                    BankAccount = s.BankAccount,
                    CompanyName = s.CompanyName,
                    TaxNumber = s.TaxNumber,
                }).FirstOrDefaultAsync(cancellationToken);
            if (supplier is null)
            {
                logger.LogWarning("Supplier with user id {Id} not found", request.userid);
                return AppErrors.NotFound("Supplier", request.userid);
            }
            return supplier;
        }
    }
}
