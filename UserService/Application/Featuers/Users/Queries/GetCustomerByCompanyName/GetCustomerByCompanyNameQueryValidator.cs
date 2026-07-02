using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetCustomerByCompanyName
{
    public class GetCustomerByCompanyNameQueryValidator:AbstractValidator<GetCustomerByCompanyNameQuery>
    {
        public GetCustomerByCompanyNameQueryValidator()
        {
            RuleFor(x => x.companyName)
                .NotEmpty()
                .WithMessage("CompanyName is required");
        }
    }
}
