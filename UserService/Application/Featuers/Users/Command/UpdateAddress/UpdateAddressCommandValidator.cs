using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.UpdateAddress
{
    public class UpdateAddressCommandValidator:AbstractValidator<UpdateAddressCommand>
    {
       public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.userid)
            .NotEmpty();

            RuleFor(x => x.Addresses)
                .NotNull()
                .NotEmpty();

            RuleForEach(x => x.Addresses)
                .ChildRules(address =>
                {
                    address.RuleFor(x => x.City)
                        .NotEmpty()
                        .MaximumLength(100);

                    address.RuleFor(x => x.Street)
                        .NotEmpty()
                        .MaximumLength(200);

                    address.RuleFor(x => x.BuildingNo)
                        .NotEmpty()
                        .MaximumLength(20);

                    address.RuleFor(x => x.PostalCode)
                        .MaximumLength(20);
                });
        }
    }
}
