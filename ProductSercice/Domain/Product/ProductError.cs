using Shared.Results;

namespace ProductSercice.Domain.Product
{
    public static class ProductError
    {
        public static Error ProductNameIsRequired=>
            Error.Validation("Product.Name.Required", "Product name is required.");
        public static Error ProductDescriptionIsRequired =>
            Error.Validation("Product.Description.Required", "Product description is required.");
        public static Error ProductSKUIsRequired =>
            Error.Validation("Product.SKU.Required", "Product SKU is required.");
        public static Error ProductBarcodeIsRequired =>
            Error.Validation("Product.Barcode.Required", "Product barcode is required.");
        public static Error ProductCategoryIdIsRequired =>
            Error.Validation("Product.CategoryId.Required", "Product category id is required.");
        public static Error ProductBrandIdIsRequired =>
            Error.Validation("Product.BrandId.Required", "Product brand id is required.");
        public static Error ProductCostPriceMustBeGreaterThanZero =>
            Error.Validation("Product.CostPrice.MustbeGreaterThanZero", "Product CostPrice Must be Greater Than Zero.");
        public static Error ProductSellingPriceMustBeGreaterThanZero =>
            Error.Validation("Product.SellingPrice.MustbeGreaterThanZero", "Product SellingPrice Must be Greater Than Zero.");
        public static Error ProductAlreadyActive =>
            Error.Conflict("Product.AlreadyActive", "Product is already active.");
        public static Error ProductAlreadyNotActive =>
            Error.Conflict("Product.AlreadyNotActive", "Product is already not active.");
    }
}
