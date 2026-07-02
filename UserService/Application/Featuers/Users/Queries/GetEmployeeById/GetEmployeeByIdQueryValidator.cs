using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
    {
        public GetEmployeeByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Employee Id is required.");
        }
    }
}
