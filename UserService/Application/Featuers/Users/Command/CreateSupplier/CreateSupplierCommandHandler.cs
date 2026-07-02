using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Featuers.Users.Mapper;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.CreateSupplier
{
    public class CreateSupplierCommandHandler(IAppDbContext context,
        ILogger<CreateSupplierCommandHandler> logger, IMemoryCache cache) : IRequestHandler<CreateSupplierCommand, Result<SupplierDto>>
    {
        public async Task<Result<SupplierDto>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var supplierResult = user.AddSupplier(Guid.NewGuid(), request.companyName, request.taxNumber,request.bankAccount);
            if (supplierResult.IsError)
            {
                logger.LogWarning("Failed to add supplier for user with id {Id}: {Errors}", request.userid, supplierResult.Errors);
                return supplierResult.Errors;
            }
            context.Suppliers.Add(supplierResult.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("suppliers");
            return supplierResult.Value.ToDto();

        }
    }
}
