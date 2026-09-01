using FluentValidation;

namespace SalesService.Application.Featuers.SalesOrdes.Queries.GetSalesOrderById
{
    public class GetSalesOrderByIdQueryValidator : AbstractValidator<GetSalesOrderByIdQuery>
    {
        public GetSalesOrderByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required.");
        }
    }
}
