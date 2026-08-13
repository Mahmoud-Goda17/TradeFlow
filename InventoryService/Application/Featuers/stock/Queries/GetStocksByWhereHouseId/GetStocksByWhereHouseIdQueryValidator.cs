using FluentValidation;

namespace InventoryService.Application.Featuers.stock.Queries.GetStocksByWhereHouseId
{
    public class GetStocksByWhereHouseIdQueryValidator : AbstractValidator<GetStocksByWhereHouseIdQuery>
    {
        public GetStocksByWhereHouseIdQueryValidator()
        {
            RuleFor(x => x.wherhouseid)
                .NotEmpty().WithMessage("Warehouse ID is required.")
                .Must(id => id != Guid.Empty).WithMessage("Warehouse ID cannot be empty.");
        }
    }
}
