using FluentValidation;

namespace ProductSercice.Application.Featuers.Brands.Queries.GetBrandById
{
    public class GetBrandByIdQueryValidator:AbstractValidator<GetBrandByIdQuery>
    {
        public GetBrandByIdQueryValidator()
        {
            RuleFor(x => x.id).NotEmpty().WithMessage("Brand Id is required.");
        }
    }
}
