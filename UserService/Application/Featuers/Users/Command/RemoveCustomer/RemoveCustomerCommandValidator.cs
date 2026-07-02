using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.RemoveCustomer
{
    public class RemoveCustomerCommandValidator : AbstractValidator<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}
