using FluentValidation;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.DeActiveWhereHouse
{
    public class DeActiveWhereHouseCommandValidator : AbstractValidator<DeActiveWhereHouseCommand>
    {
        public DeActiveWhereHouseCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("WhereHouse id is required.")
                .NotEqual(Guid.Empty).WithMessage("WhereHouse id cannot be empty.");
        }
    }
}
