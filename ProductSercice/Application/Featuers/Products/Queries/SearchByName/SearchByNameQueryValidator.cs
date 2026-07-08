using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Queries.SearchByName
{
    public class SearchByNameQueryValidator: AbstractValidator<SearchByNameQuery>
    {
        public SearchByNameQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product Name is required.")
                .MinimumLength(3).WithMessage("Product Name must be at least 3 characters long.");
        }
    }
}
