using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Queries.GetProductById
{
    public class GetProductByIdQueryValidator:AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id).
                NotEmpty().WithMessage("Product Id is required.");
        }
    }
}
