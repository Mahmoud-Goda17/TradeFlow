using ProductSercice.Domain.Product;

namespace ProductSercice.Application.Featuers.Products.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get;  set; } = default!;

        public string Description { get;  set; } = default!;

        public string SKU { get; set; } = default!;

        public string Barcode { get;  set; } = default!;
        public decimal CostPrice { get; set; }

        public decimal SellingPrice { get; set; }

        public ProductStatus Status { get; set; }

    }
}
