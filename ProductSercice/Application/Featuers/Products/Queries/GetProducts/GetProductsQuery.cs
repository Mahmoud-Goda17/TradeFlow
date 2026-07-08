using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetProducts
{
    public sealed record class GetProductsQuery : IRequest<Result<List<ProductDto>>>
    {
    }
}
