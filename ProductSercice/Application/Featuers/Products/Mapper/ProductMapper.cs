
using System.Linq.Expressions;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Domain.Product;

namespace ProductSercice.Application.Featuers.Products.Mapper
{
    public static class ProductMapper
    {
        public static readonly Expression<Func<Product, ProductDto>> ToDto =
                p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    SKU = p.SKU,
                    Barcode = p.Barcode,
                    CostPrice = p.CostPrice,
                    SellingPrice = p.SellingPrice,
                    Status = p.Status
                };
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                SKU = product.SKU,
                Barcode = product.Barcode,
                CostPrice = product.CostPrice,
                SellingPrice = product.SellingPrice,
                Status = product.Status
            };
        }
    }
}
