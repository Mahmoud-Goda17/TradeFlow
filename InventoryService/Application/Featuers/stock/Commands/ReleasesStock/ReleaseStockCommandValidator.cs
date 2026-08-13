using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Commands.ReleasesStock
{
    public class ReleaseStockCommandValidator : AbstractValidator<ReleaseStockCommand>
    {
        public ReleaseStockCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
