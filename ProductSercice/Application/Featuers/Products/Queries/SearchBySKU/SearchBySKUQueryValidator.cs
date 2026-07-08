using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Queries.SearchBySKU
{
    public class SearchBySKUQueryValidator: AbstractValidator<SearchBySKUQuery>
    {
        public SearchBySKUQueryValidator()
        {
            RuleFor(x => x.SKU)
                .NotEmpty().WithMessage("Product SKU is required.")
                .Length(3,50).WithMessage("Product SKU must be between 3 to 50 characters .");
        }
    }
}
