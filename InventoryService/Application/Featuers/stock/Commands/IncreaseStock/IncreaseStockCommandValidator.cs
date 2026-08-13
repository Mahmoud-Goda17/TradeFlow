using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Commands.IncreaseStock
{
    public class IncreaseStockCommandValidator : AbstractValidator<IncreaseStockCommand>
    {
        public IncreaseStockCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
