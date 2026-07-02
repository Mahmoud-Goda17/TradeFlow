using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.NotActiveUser
{
    public class NotActiveUserCommandValidator :
         AbstractValidator<NotActiveUserCommand>
    {
        public NotActiveUserCommandValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty().WithMessage("User ID is required.");
        }
   
    }
}
