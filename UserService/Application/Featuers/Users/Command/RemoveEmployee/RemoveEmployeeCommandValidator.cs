using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.RemoveEmployee
{
    public class RemoveEmployeeCommandValidator:AbstractValidator<RemoveEmployeeCommand>
    {
        public RemoveEmployeeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required");
        }
    }
}
