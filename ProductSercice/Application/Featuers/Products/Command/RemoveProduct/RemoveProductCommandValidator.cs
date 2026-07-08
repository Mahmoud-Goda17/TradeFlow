using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Command.RemoveProduct
{
    public class RemoveProductCommandValidator: AbstractValidator<RemoveProductCommand>
    {
        public RemoveProductCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required");
        }
    }
}
