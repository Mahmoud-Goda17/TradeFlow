using FluentValidation;

namespace SalesService.Application.Featuers.SalesOrdes.Commands.AddItemToSalesOrder
{
    public class AddItemToSalesOrderCommandValidator:AbstractValidator<AddItemToSalesOrderCommand>
    {
        public AddItemToSalesOrderCommandValidator()
        {
            RuleFor(x => x.salesorderid)
                .NotEmpty().WithMessage("salesorderid is required");
            RuleFor(x => x.productId)
                .NotEmpty().WithMessage("productId is required");
            RuleFor(x => x.quantity)
                .GreaterThan(0).WithMessage("quantity must be greater than 0");
            RuleFor(x => x.unitPrice)
                .GreaterThan(0).WithMessage("unitPrice must be greater than 0");
            RuleFor(x => x.discount)
                .GreaterThanOrEqualTo(0).WithMessage("discount must be greater than or equal to 0");
            RuleFor(x => x.tax)
                .GreaterThanOrEqualTo(0).WithMessage("tax must be greater than or equal to 0");


        }

    }
}
