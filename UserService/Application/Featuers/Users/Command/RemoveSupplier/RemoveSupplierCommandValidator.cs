using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.RemoveSupplier
{
    public class RemoveSupplierCommandValidator : AbstractValidator<RemoveSupplierCommand>
    {
        public RemoveSupplierCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}
