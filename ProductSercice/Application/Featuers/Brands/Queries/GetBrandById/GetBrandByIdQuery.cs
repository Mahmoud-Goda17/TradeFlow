using MediatR;
using ProductSercice.Application.Featuers.Brands.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Queries.GetBrandById
{
    public sealed record  GetBrandByIdQuery(Guid id) : IRequest<Result<BrandDto>>
    {
    }
}
