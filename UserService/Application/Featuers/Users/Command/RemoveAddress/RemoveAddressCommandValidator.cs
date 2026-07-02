using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.RemoveAddress
{
    public class RemoveAddressCommandValidator:AbstractValidator<RemoveAddressCommand>
    {
      public  RemoveAddressCommandValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty().WithMessage("user id is required");
        }
    }
}
