using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.RemoveUser
{
    public class RemoveUserCommandValidator :
         AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }
}
