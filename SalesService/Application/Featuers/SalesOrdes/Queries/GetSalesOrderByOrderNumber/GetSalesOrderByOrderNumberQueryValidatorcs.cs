using FluentValidation;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderByOrderNumber
{
    public class GetSalesOrderByOrderNumberQueryValidatorcs : AbstractValidator<GetSalesOrderByOrderNumberQuery>
    {
        public GetSalesOrderByOrderNumberQueryValidatorcs()
        {
            RuleFor(x => x.OrderNumber).NotEmpty().WithMessage("OrderNumber is required.");
        }
    }
}
