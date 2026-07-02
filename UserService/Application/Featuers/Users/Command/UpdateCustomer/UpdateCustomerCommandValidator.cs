using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.usreid)
                .NotEmpty().WithMessage("User Id is required.");
            RuleFor(x => x.companyName)
                .NotEmpty().WithMessage("company name is required.")
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");
            RuleFor(x => x.taxNumber)
                .NotEmpty().WithMessage("Text Number is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
