using FluentValidation;

namespace ProductSercice.Application.Featuers.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Category Id is required.");
            RuleFor(x => x.name).NotEmpty().WithMessage("Category name is required.");
            RuleFor(x => x.description).NotEmpty().WithMessage("Category description is required.");
        }
    }
}
