using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator :
        AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().
                WithMessage("User id is required.");
        }
    }
}
