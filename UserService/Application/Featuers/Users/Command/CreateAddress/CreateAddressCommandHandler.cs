using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Featuers.Users.Mapper;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Command.CreateAddress
{
    public class CreateAddressCommandHandler(IAppDbContext context,
        ILogger<CreateAddressCommandHandler> logger) : IRequestHandler<CreateAddressCommand, Result<AddressDto>>
    {
        public async Task<Result<AddressDto>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == request.userid, cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found.", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var address = user.AddAddress(Guid.NewGuid(), request.city, request.street, request.buildingNo, request.postalCode);
            if (address.IsError)
            {
                logger.LogWarning("Failed to add address for user with id {Id}: {Errors}", request.userid, address.Errors);
                return address.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            return address.Value.ToDto();
        }
    
    }
}
