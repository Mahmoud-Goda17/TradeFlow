using FluentValidation;

namespace ProductSercice.Application.Featuers.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
    {
        public RemoveCategoryCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Category Id is required.");
        }
    }
}
