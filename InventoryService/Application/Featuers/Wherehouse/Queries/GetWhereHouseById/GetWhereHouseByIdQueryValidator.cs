using FluentValidation;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseById
{
    public class GetWhereHouseByIdQueryValidator: AbstractValidator<GetWhereHouseByIdQuery>
    {
        public GetWhereHouseByIdQueryValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Wherehouse Id is required.");
        }
    }
}
