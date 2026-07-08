using FluentValidation;

namespace ProductSercice.Application.Featuers.Brands.Queries.SearchByName
{
    public class SearchByNameQueryValidator : AbstractValidator<SearchByNameQuery>
    {
        public SearchByNameQueryValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        }
    }
}
