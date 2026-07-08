using FluentValidation;

namespace ProductSercice.Application.Featuers.Brands.Commands.RemoveBrand
{
    public class RemoveBrandCommandValidator : AbstractValidator<RemoveBrandCommand>
    {
        public RemoveBrandCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Brand id is required.")
                .NotEqual(Guid.Empty).WithMessage("Brand id cannot be empty.");
        }
    }
}
