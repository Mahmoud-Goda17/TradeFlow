using MediatR;
using ProductSercice.Application.Featuers.Brands.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Queries.GetBrands
{
    public sealed record GetBrandsQuery : IRequest<Result<List<BrandDto>>>
    {
    }
}
