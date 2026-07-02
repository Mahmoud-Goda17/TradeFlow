using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.CreateSupplier
{
    public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty().WithMessage("User Id is required.");
            RuleFor(x => x.companyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(100).WithMessage("Company name must not exceed 100 characters.");
            RuleFor(x => x.taxNumber)
                .NotEmpty().WithMessage("Tax Number is required.")
                .MaximumLength(50).WithMessage("Tax Number must not exceed 50 characters.");
            RuleFor(x=>x.bankAccount)
                .NotEmpty().WithMessage("Bank Account is required.")
                .MaximumLength(50).WithMessage("Bank Account must not exceed 50 characters.");
        }
    }
}
