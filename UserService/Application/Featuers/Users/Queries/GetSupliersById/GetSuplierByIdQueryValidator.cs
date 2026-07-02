using FluentValidation;

namespace UserService.Application.Featuers.Users.Queries.GetSupliersById
{
    public class GetSuplierByIdQueryValidator : AbstractValidator<GetSuplierByIdQuery>
    {
        public GetSuplierByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Supplier Id is required.");
        }
    }
}
