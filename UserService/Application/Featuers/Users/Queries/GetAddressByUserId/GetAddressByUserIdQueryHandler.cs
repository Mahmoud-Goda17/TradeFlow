using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetAddressByUserId
{
    public class GetAddressByUserIdQueryHandler(IAppDbContext context,
        ILogger<GetAddressByUserIdQueryHandler> logger) : IRequestHandler<GetAddressByUserIdQuery, Result<List<AddressDto>>>
    {
        public async Task<Result<List<AddressDto>>> Handle(GetAddressByUserIdQuery request, CancellationToken cancellationToken)
        {
            var user= await context.Users.AnyAsync(u => u.Id == request.userid, cancellationToken);
            if (!user)
            {
                logger.LogWarning("User with id {Id} not found", request.userid);
                return AppErrors.NotFound("User", request.userid);
            }
            var addresses = await context.Addresses.Where(a => a.UserId == request.userid)
                .Select(a => new AddressDto
                {
                    Street = a.Street,
                    City = a.City,
                    BuildingNo = a.BuildingNo,
                }).ToListAsync(cancellationToken);
            return addresses;
        }
    }
}
