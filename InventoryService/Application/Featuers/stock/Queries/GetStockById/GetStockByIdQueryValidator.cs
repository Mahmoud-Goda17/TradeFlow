using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Queries.GetStockById
{
    public class GetStockByIdQueryValidator:AbstractValidator<GetStockByIdQuery>
    {
        public GetStockByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required");
        }
    }
}
