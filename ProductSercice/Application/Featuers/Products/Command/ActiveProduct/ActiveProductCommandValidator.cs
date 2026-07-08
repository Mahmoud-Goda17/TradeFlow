using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Command.ActiveProduct
{
    public class ActiveProductCommandValidator : AbstractValidator<ActiveProductCommand>
    {
        public ActiveProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required");
        }
    }
}
