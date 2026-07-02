using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Featuers.Users.Mapper;
using UserService.Application.Interfaces;

namespace UserService.Application.Featuers.Users.Queries.GetUsers
{
    public class GetUserQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetUsersQuery, Result<List<UserDto>>>
    {
        public async Task<Result<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var cacheusers = await cache.GetOrCreateAsync("users", async entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                var users = await context.Users.AsNoTracking().ToListAsync(cancellationToken);
                return users.ToDtoList();
            });
            return cacheusers;
        }
    }
}
