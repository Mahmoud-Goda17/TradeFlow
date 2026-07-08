using FluentValidation;

namespace ProductSercice.Application.Featuers.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Category name is required.");
            RuleFor(x => x.description).NotEmpty().WithMessage("Category description is required.");
        }
    }
}
