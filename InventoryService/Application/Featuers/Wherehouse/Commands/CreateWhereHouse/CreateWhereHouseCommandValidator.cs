using FluentValidation;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.CreateWhereHouse
{
    public class CreateWhereHouseCommandValidator : AbstractValidator<CreateWhereHouseCommand>
    {
        public CreateWhereHouseCommandValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");
            RuleFor(x => x.address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");
        }
    }
}
