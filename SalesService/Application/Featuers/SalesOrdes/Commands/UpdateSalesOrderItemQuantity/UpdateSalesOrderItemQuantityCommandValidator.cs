using FluentValidation;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.UpdateSalesOrderItemQuantity
{
    public class UpdateSalesOrderItemQuantityCommandValidator: AbstractValidator<UpdateSalesOrderItemQuantityCommand>
    {
        public UpdateSalesOrderItemQuantityCommandValidator()
        {
                    RuleFor(x => x.salesorderid)
                .NotEmpty().WithMessage("Sales order ID is required.");
            RuleFor(x => x.productId)
                    .NotEmpty().WithMessage("Product ID is required.");
            RuleFor(x => x.newQuantity)
                    .GreaterThanOrEqualTo(0).WithMessage("New Quantity must be greater than or equal to 0.");



        }
    }
}
