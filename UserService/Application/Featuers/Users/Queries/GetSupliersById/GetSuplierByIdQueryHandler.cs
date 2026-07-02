using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetSupliersById
{
    public class GetSuplierByIdQueryHandler(IAppDbContext context,
        ILogger<GetSuplierByIdQueryHandler> logger) : IRequestHandler<GetSuplierByIdQuery, Result<SupplierDto>>
    {
        public async Task<Result<SupplierDto>> Handle(GetSuplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await context.Suppliers.Where(s => s.Id == request.Id)
                .Select(s => new SupplierDto
                {
                    CompanyName = s.CompanyName,
                    TaxNumber = s.TaxNumber
                }).FirstOrDefaultAsync(cancellationToken);
            if (supplier == null)
            {
                logger.LogWarning("Supplier with id {Id} not found", request.Id);
                return AppErrors.NotFound("Supplier", request.Id);
            }
            return supplier;
        }
    }
}
