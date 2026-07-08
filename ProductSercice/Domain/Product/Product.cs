using Shared.Commen;
using Shared.Results;

namespace ProductSercice.Domain.Product
{
    public sealed class Product : AuditableEntity
    {
        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;

        public string SKU { get; private set; } = default!;

        public string Barcode { get; private set; } = default!;

        public Guid CategoryId { get; private set; }

        public Guid BrandId { get; private set; }

        public decimal CostPrice { get; private set; }

        public decimal SellingPrice { get; private set; }

        public ProductStatus Status { get; private set; }
        public Category.Category Category { get; private set; } = default!;
        public Brand.Brand Brand { get; private set; } = default!;
        private Product() { }
        private Product(Guid id, string name, string description, string sKU, string barcode, Guid categoryId, Guid brandId, decimal costPrice, decimal sellingPrice, ProductStatus status) : base(id)
        {
            Name = name;
            Description = description;
            SKU = sKU;
            Barcode = barcode;
            CategoryId = categoryId;
            BrandId = brandId;
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
            Status = status;
        }
        public static Result<Product> Create(Guid id, string name, string description, string sKU, string barcode, Guid categoryId, Guid brandId, decimal costPrice, decimal sellingPrice, ProductStatus status)
        {
            var errors = Errors(name, description, sKU, barcode, categoryId, brandId, costPrice, sellingPrice);
            if (errors.Count > 0)
            {
                return errors;
            }
            var product = new Product(id, name, description, sKU, barcode, categoryId, brandId, costPrice, sellingPrice, status);
            return product;
        }
        public Result<Updated> Update(string name, string description, string sKU, string barcode, Guid categoryId, Guid brandId, decimal costPrice, decimal sellingPrice)
        {
            var errors = Errors(name, description, sKU, barcode, categoryId, brandId, costPrice, sellingPrice);
            if (errors.Count > 0)
            {
                return errors;
            }
            Name = name;
            Description = description;
            SKU = sKU;
            Barcode = barcode;
            CategoryId = categoryId;
            BrandId = brandId;
            CostPrice = costPrice;
            SellingPrice = sellingPrice;
            return Result.Updated;
        }
        public Result<Updated> Activate()
        {
            if (Status == ProductStatus.Active)
            {
                return ProductError.ProductAlreadyActive;
            }
            Status = ProductStatus.Active;
            return Result.Updated;
        }
        public Result<Updated> Deactivate()
        {
            if (Status == ProductStatus.Inactive)
            {
                return ProductError.ProductAlreadyNotActive;
            }
            Status = ProductStatus.Inactive;
            return Result.Updated;
        }
        private static List<Error> Errors(string name, string description, string sKU, string barcode, Guid categoryId, Guid brandId, decimal costPrice, decimal sellingPrice)
        {
            var errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(ProductError.ProductNameIsRequired);
            }
            if (string.IsNullOrWhiteSpace(description))
            {
                errors.Add(ProductError.ProductDescriptionIsRequired);
            }
            if (string.IsNullOrWhiteSpace(sKU))
            {
                errors.Add(ProductError.ProductSKUIsRequired);
            }
            if (string.IsNullOrWhiteSpace(barcode))
            {
                errors.Add(ProductError.ProductBarcodeIsRequired);
            }
            if (categoryId == Guid.Empty)
            {
                errors.Add(ProductError.ProductCategoryIdIsRequired);
            }
            if (brandId == Guid.Empty)
            {
                errors.Add(ProductError.ProductBrandIdIsRequired);
            }
            if (costPrice <= 0)
            {
                errors.Add(ProductError.ProductCostPriceMustBeGreaterThanZero);
            }
            if (sellingPrice <= 0)
            {
                errors.Add(ProductError.ProductSellingPriceMustBeGreaterThanZero);
            }
            return errors;
        }
    }
}