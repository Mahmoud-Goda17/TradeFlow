using FluentValidation;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.RemoveSalesOrderItem
{
    public class RemoveSalesOrderItemCommandValidator: AbstractValidator<RemoveSalesOrderItemCommand>
    {
        public RemoveSalesOrderItemCommandValidator()
        {
            RuleFor(x => x.salesorderid)
                .NotEmpty().WithMessage("Sales order ID is required.");
            RuleFor(x => x.productId)
                .NotEmpty().WithMessage("Product ID is required.");
        }
    }
}
