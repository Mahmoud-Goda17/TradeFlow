using UserService.Domain.Users;

namespace UserService.Application.Featuers.Users.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get;  set; } = default!;

        public string Email { get;  set; } = default!;

        public string Phone { get; set; } = default!;

        public UserType UserType { get; set; }

        public bool IsActive { get; set; }

    }
}
