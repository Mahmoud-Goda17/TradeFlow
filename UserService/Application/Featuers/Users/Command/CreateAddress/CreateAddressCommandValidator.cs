using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.CreateAddress
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId cannot be empty.");
            RuleFor(x => x.city)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");
            RuleFor(x => x.street)
                .NotEmpty().WithMessage("Street is required.")
                .MaximumLength(200).WithMessage("Street cannot exceed 200 characters.");
            RuleFor(x => x.buildingNo)
                .NotEmpty().WithMessage("Building number is required.")
                .MaximumLength(50).WithMessage("Building number cannot exceed 50 characters.");
            RuleFor(x => x.postalCode)
                .MaximumLength(20).WithMessage("Postal code cannot exceed 20 characters.");
        }
    }
}
