using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Queries.GetStockByProductId
{
    public class GetStockByProductIdQueryValidator : AbstractValidator<GetStockByProductIdQuery>
    {
        public GetStockByProductIdQueryValidator()
        {
            RuleFor(x => x.productid)
                .NotEmpty().WithMessage("ProductId is required.")
                .NotEqual(Guid.Empty).WithMessage("ProductId cannot be empty.");
        }
    }
}
