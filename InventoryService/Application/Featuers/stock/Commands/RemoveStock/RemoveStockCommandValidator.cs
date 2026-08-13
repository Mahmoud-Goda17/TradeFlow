using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Commands.RemoveStock
{
    public class RemoveStockCommandValidator : AbstractValidator<RemoveStockCommand>
    {
        public RemoveStockCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
