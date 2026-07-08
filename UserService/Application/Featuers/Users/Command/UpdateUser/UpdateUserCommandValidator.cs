using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.UpdateUser
{
    public class UpdateUserCommandValidator :
         AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.fullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^01[0125][0-9]{8}$").WithMessage("Phone number must be 7–15 digits and may start with '+'.");
            RuleFor(x => x.userType)
                .IsInEnum().WithMessage("Invalid user type.");
        }
    }
}
