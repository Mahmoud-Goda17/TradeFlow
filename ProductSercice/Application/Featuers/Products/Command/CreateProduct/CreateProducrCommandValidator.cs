using FluentValidation;

namespace ProductSercice.Application.Featuers.Products.Command.CreateProduct
{
    public class CreateProducrCommandValidator:AbstractValidator<CreateProducrCommand>
    {
        public CreateProducrCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.sKU)
                .NotEmpty().WithMessage("SKU is required")
                .Length(3, 50).WithMessage("Product SKU must be between 3 to 50 characters .");
            RuleFor(x => x.barcode)
               .NotEmpty().WithMessage("Barcode is required")
               .Length(3, 18).WithMessage("Product BarCode must be between 3 to 18 characters .");
            RuleFor(x => x.categoryId).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.brandId).NotEmpty().WithMessage("Brand is required");
            RuleFor(x => x.costPrice)
                .NotEmpty().WithMessage("Cost Price is required")
                .GreaterThan(0).WithMessage("Cost Price must be greater than 0");
            RuleFor(x => x.sellingPric)
                .NotEmpty().WithMessage("Selling Price is required")
                .GreaterThan(0).WithMessage("Selling Price must be greater than 0");
            RuleFor(x => x.status).IsInEnum().WithMessage("Status is required");

        }
    }
}
