using FluentValidation;

namespace ProductSercice.Application.Featuers.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Category Id is required.");
        }
    }
}
