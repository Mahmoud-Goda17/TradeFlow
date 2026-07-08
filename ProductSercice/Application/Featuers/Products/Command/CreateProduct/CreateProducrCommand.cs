using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Domain.Product;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.CreateProduct
{
    public sealed record class CreateProducrCommand(string name,
        string description,
        string sKU,
        string barcode,
        Guid categoryId,
        Guid brandId,decimal costPrice,decimal sellingPric, ProductStatus status):IRequest<Result<ProductDto>>
    {
    }
}
