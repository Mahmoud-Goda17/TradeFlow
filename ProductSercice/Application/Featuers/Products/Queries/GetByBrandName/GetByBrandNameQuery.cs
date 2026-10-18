using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByBrandName
{
    public sealed record  GetByBrandNameQuery(string brandname) : IRequest<Result<List<ProductDto>>>
    {
    }
}
