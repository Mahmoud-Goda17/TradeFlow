using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetSupplierByCompanyName
{
    public class GetSupplierByCompanyNameQueryValidator:AbstractValidator<GetSupplierByCompanyNameQuery>
    {
        public GetSupplierByCompanyNameQueryValidator()
        {
            RuleFor(s => s.companyname)
                .NotEmpty()
                .WithMessage("CompanyName is Required");
        }
    }
}
