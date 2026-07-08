using FluentValidation;

namespace ProductSercice.Application.Featuers.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandValidator: AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Brand Id is required.");
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
            RuleFor(x => x.country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(100).WithMessage("Country must not exceed 100 characters.");
        }
    }
}
