using FluentValidation;

namespace InventoryService.Application.Featuers.Wherehouse.Commands.RemoveWhereHouse
{
    public class RemoveWhereHouseCommandValidator : AbstractValidator<RemoveWhereHouseCommand>
    {
        public RemoveWhereHouseCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
