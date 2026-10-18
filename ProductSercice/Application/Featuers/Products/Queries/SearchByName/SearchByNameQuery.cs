using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.SearchByName
{
    public sealed record  SearchByNameQuery(string Name) : IRequest<Result<List<ProductDto>>>
    {
    }
}
