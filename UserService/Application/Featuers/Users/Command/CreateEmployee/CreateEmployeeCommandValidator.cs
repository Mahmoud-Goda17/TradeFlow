using FluentValidation;

namespace UserService.Application.Featuers.Users.Command.CreateEmployee
{
    public class CreateEmployeeCommandValidator:AbstractValidator<CreateEmployeeCommand>
    {
       public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.userId).NotEmpty()
                .WithMessage("Id is required");
            RuleFor(x => x.department)
                   .NotEmpty()
                   .MaximumLength(100);

            RuleFor(x => x.jobTitle)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.salary)
                .GreaterThan(0)
                .LessThanOrEqualTo(1_000_000);

            RuleFor(x => x.hireDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTimeOffset.UtcNow)
                .WithMessage("Hire date cannot be in the future.");
        }
    }
}
