using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Commands.DecreaseStock
{
    public class DecreaseStockCommandValidator : AbstractValidator<DecreaseStockCommand>
    {
        public DecreaseStockCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
