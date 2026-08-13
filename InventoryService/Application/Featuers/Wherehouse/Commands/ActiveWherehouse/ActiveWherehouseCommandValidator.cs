using FluentValidation;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.ActiveWherehouse
{
    public class ActiveWherehouseCommandValidator : AbstractValidator<ActiveWherehouseCommand>
    {
        public ActiveWherehouseCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.")
                .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
        }
    }
}
