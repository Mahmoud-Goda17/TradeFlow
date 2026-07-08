using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByCategoryName
{
    public class GetByCategoryNameQueryValidator: AbstractValidator<GetByCategoryNameQuery>
    {
        public GetByCategoryNameQueryValidator()
        {
            RuleFor(x => x.categoryname)
                .NotEmpty().WithMessage("Category Name is required.")
                .MinimumLength(3).WithMessage("Category Name must be at least 3 characters long.");
        }
    }
}
