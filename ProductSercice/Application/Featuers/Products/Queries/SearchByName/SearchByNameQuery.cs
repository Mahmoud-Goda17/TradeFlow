using MediatR;
using ProductSercice.Application.Featuers.Brands.Dtos;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.SearchByName
{
    public sealed record class SearchByNameQuery(string Name) : IRequest<Result<List<ProductDto>>>
    {
    }
}
