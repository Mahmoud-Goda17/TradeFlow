using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Commands.CreateStock
{
    public class CreateStockCommandValidator : AbstractValidator<CreateStockCommand>
    {
        public CreateStockCommandValidator()
        {
            RuleFor(x => x.productId)
                .NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.warehouseId)
                .NotEmpty().WithMessage("WarehouseId is required.");
            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
