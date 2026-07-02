using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetAddressByUserId
{
    public class GetAddressByUserIdQueryValidator : AbstractValidator<GetAddressByUserIdQuery>
    {
        public GetAddressByUserIdQueryValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty()
                .WithMessage("User Id is required.");
        }
    }
}
