using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetUserByFullName
{
    public class GetUserByFullNameQueryValidator : AbstractValidator<GetUserByFullNameQuery>
    {
        public GetUserByFullNameQueryValidator()
        {
            RuleFor(x => x.fullname)
                .NotEmpty()
                .WithMessage("FullName is required");
        }
    }
}
