using MediatR;
using ProductSercice.Application.Featuers.Brands.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Commands.CreateBrand
{
    public sealed record class CreateBrandCommand(string name, string description, string country) : IRequest<Result<BrandDto>>
    {
    }
}
