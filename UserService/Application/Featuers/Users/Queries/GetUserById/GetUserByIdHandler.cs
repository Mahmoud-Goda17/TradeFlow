using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetUserById
{
    public class GetUserByIdHandler(IAppDbContext context,
        ILogger<GetUserByIdHandler> logger) : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.Where(u => u.Id == request.Id)
                .Select(u => new UserDto
                {
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    IsActive = u.IsActive,
                    UserType = u.UserType
                }).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                logger.LogWarning("User with id {Id} not found", request.Id);
                return AppErrors.NotFound("User",request.Id);
            }
            return user;
        }
    }

}
