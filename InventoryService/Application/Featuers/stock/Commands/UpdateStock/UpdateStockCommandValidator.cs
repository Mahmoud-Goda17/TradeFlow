using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Commands.UpdateStock
{
    public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.productid)
                .NotEmpty().WithMessage("ProductId is required.");
            RuleFor(x => x.warehouseId)
                .NotEmpty().WithMessage("WarehouseId is required.");
            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
