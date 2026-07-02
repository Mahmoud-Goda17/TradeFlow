using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetEmployeeByUserId
{
    public class GetEmployeeByUserIdQueryValidator : AbstractValidator<GetEmployeeByUserIdQuery>
    {
        public GetEmployeeByUserIdQueryValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty()
                .WithMessage("UserId is required");
        }
    }
}
