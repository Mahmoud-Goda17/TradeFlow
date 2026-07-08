using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Featuers.Brands.Dtos;
using ProductSercice.Application.Featuers.Brands.Mapper;
using ProductSercice.Application.Interfaces;
using ProductSercice.Domain.Brand;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler(IAppDbContext context,
        ILogger<CreateBrandCommandHandler> logger, IMemoryCache cache) : IRequestHandler<CreateBrandCommand, Result<BrandDto>>
    {
        public async Task<Result<BrandDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var name= request.name.Trim();
            var isexcist = await context.brands.AnyAsync(b => b.Name == name);
            if (isexcist)
            {
                logger.LogWarning("Brand with name {name} already exists", name);
                return ProductAppError.IsExcist("Brand", "name", name);
            }
            var newBrand = Brand.Create(Guid.NewGuid(), name,request.description,request.country);
            if (newBrand.IsError)
            {
                logger.LogWarning("Failed to create brand: {Errors}", newBrand.Errors);
                return newBrand.Errors;
            }
            context.brands.Add(newBrand.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("brands");
            return newBrand.Value.ToBrandDto();
        }
    }
}
