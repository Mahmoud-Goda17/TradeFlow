using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryValidator :
        AbstractValidator<GetCutomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Customer id is required.");
        }
    }
}
