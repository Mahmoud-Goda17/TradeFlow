using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.ActiveUser
{
    public class ActiveUserCommandValidator :
         AbstractValidator<ActiveUserCommand>
    {
        public ActiveUserCommandValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty().WithMessage("User ID is required.");
        }
    
    }
}
