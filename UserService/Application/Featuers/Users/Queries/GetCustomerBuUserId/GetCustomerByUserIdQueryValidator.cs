using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerBuUserId
{
    public class GetCustomerByUserIdQueryValidator : AbstractValidator<GetCustomerByUserIdQuery>
    {
        public GetCustomerByUserIdQueryValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty()
                .WithMessage("UserId is required");
        }
    }
}
