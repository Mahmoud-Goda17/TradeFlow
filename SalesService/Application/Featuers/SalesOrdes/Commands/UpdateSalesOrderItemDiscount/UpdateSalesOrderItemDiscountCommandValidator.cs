using FluentValidation;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemDiscount
{
    public class UpdateSalesOrderItemDiscountCommandValidator : AbstractValidator<UpdateSalesOrderItemDiscountCommand>
    {
        public UpdateSalesOrderItemDiscountCommandValidator()
        {
            RuleFor(x => x.salesorderid)
                .NotEmpty().WithMessage("Sales order ID is required.");
            RuleFor(x => x.productId)
                .NotEmpty().WithMessage("Product ID is required.");
            RuleFor(x => x.newDiscount)
                .GreaterThanOrEqualTo(0).WithMessage("New discount must be greater than or equal to 0.");
        }
    }
}
