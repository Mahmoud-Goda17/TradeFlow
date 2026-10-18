using MediatR;
using ProductSercice.Application.Featuers.Brands.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Queries.SearchByName
{
    public sealed record SearchByNameQuery(string name) : IRequest<Result<List<BrandDto>>>
    {
    }
}
