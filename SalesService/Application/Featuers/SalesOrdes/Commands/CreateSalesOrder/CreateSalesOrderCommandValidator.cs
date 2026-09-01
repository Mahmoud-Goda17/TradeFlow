using FluentValidation;
using SalesService.Application.Featuers.SalesOrdes.Commands.AddItemToSalesOrder;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.CreateSalesOrder
{
    public class CreateSalesOrderCommandValidator: AbstractValidator<CreateSalesOrderCommand>
    {
        public CreateSalesOrderCommandValidator()
        {
            RuleFor(x => x.orderNumber)
                .NotEmpty().WithMessage("Order number is required.")
                .MaximumLength(50).WithMessage("Order number must not exceed 50 characters.");
            RuleFor(x => x.customerId)
                .NotEmpty().WithMessage("Customer ID is required.");
            RuleFor(x => x.shippingAddress)
                .NotEmpty().WithMessage("Shipping address is required.")
                .MaximumLength(200).WithMessage("Shipping address must not exceed 200 characters.");
            RuleForEach(x => x.items).SetValidator(new AddItemToSalesOrderCommandValidator());
        }
    }
}
