using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetSupplierByUserId
{
    public class GetSupplierByUserIdQueryValidator:AbstractValidator<GetSupplierByUserIdQuery>
    {
        public GetSupplierByUserIdQueryValidator()
        {
            RuleFor(x => x.userid)
                .NotEmpty()
                .WithMessage("UserId is required");
        }
    }
}
