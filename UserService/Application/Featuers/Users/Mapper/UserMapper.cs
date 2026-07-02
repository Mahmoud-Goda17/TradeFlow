using UserService.Application.Featuers.Users.Dtos;
using UserService.Domain.Users;

namespace UserService.Application.Featuers.Users.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                UserType = user.UserType,
                IsActive = user.IsActive
            };
        }
        public static List<UserDto> ToDtoList(this IEnumerable<User> users)
        {
            return users.Select(u=>u.ToDto()).ToList();
        }
    }
}
