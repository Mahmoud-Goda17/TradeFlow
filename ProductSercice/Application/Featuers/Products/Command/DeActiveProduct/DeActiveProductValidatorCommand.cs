using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Command.DeActiveProduct
{
    public class DeActiveProductValidatorCommand : AbstractValidator<DeActiveProductCommand>
    {
        public DeActiveProductValidatorCommand()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Id is required");
        }
    }
}
