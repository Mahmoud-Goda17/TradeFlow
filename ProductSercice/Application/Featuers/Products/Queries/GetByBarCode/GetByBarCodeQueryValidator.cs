using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByBarCode
{
    public class GetByBarCodeQueryValidator: AbstractValidator<GetByBarCodeQuery>
    {
        public GetByBarCodeQueryValidator()
        {
            RuleFor(x => x.barcode)
                .NotEmpty().WithMessage("Product Barcode is required.")
                .Length(3, 18).WithMessage("Product Barcode must be between 3 to 18characters.");
        }
    }
}
