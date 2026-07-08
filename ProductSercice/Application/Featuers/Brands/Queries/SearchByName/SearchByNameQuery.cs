using MediatR;
using ProductSercice.Application.Featuers.Brands.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Queries.SearchByName
{
    public sealed record class SearchByNameQuery(string name) : IRequest<Result<List<BrandDto>>>
    {
    }
}
