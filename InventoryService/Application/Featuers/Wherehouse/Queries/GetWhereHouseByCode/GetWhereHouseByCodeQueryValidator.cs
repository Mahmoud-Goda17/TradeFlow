using FluentValidation;

namespace InventoryService.Application.Featuers.Wherehouse.Queries.GetWhereHouseByCode
{
    public class GetWhereHouseByCodeQueryValidator : AbstractValidator<GetWhereHouseByCodeQuery>
    {
        public GetWhereHouseByCodeQueryValidator()
        {
            RuleFor(x => x.code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");
        }
    }
}
