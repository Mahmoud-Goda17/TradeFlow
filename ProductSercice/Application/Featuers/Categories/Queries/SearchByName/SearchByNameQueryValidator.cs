using FluentValidation;

namespace ProductSercice.Application.Featuers.Categories.Queries.SearchByName
{
    public class SearchByNameQueryValidator : AbstractValidator<SearchByNameQuery>
    {
        public SearchByNameQueryValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Category name is required.");
        }
    }
}
