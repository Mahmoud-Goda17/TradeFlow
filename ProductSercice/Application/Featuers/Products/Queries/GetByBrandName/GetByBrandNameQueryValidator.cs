using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByBrandName
{
    public class GetByBrandNameQueryValidator: AbstractValidator<GetByBrandNameQuery>
    {
        public GetByBrandNameQueryValidator()
        {
            RuleFor(x => x.brandname)
                .NotEmpty().WithMessage("Brand Name is required.")
                .MinimumLength(3).WithMessage("Brand Name must be at least 3 characters long.");
        }
    }
}
