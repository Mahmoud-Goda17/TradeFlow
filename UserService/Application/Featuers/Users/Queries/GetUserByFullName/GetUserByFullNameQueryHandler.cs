using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetUserByFullName
{
    public class GetUserByFullNameQueryHandler(IAppDbContext context
        ) : IRequestHandler<GetUserByFullNameQuery, Result<List<UserDto>>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetUserByFullNameQuery request, CancellationToken cancellationToken)
        {
            var name= request.fullname.Trim().ToLower();
            var users= await context.Users.Where(u=>u.FullName.ToLower().Contains(name))
                .Select(u=>new UserDto
                {
                    Email=u.Email,
                    FullName=u.FullName,
                    IsActive=u.IsActive,
                    Phone=u.Phone,
                    UserType=u.UserType,
                }).ToListAsync(cancellationToken);
            return users;
        }
    }
}
