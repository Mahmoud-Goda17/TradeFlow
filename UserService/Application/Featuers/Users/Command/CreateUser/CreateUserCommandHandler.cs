using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Shared.Results;
using UserService.Application.Featuers.Users.Dtos;
using UserService.Application.Featuers.Users.Mapper;
using UserService.Application.Interfaces;
using UserService.Domain.Users;

namespace UserService.Application.Featuers.Users.Command.CreateUser
{
    public class CreateUserCommandHandler(IAppDbContext context,
        ILogger<CreateUserCommandHandler> logger,
        IMemoryCache cache) : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        public async Task<Result<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var email= request.email.Trim().ToLower();
            var existingUser = await context.Users.AnyAsync(u => u.Email.ToLower() == email, cancellationToken);
            if (existingUser)
            {
                logger.LogWarning("User with email {Email} already exists.", email);
                return AppErrors.IsExcist("User", "Email", email);
            }

            var user =  User.Create(Guid.NewGuid(), request.fullName, email, request.phone, request.userType, request.isActive);
            if (user.IsError)
            {
                logger.LogWarning("Failed to create user: {Errors}", user.Errors);
                return user.Errors;
            }
            context.Users.Add(user.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("users");
            return user.Value.ToDto();
        }
    }
}
