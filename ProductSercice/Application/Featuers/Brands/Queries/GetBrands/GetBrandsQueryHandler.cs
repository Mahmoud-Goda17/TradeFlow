using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Featuers.Brands.Dtos;
using ProductSercice.Application.Featuers.Brands.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Queries.GetBrands
{
    public class GetBrandsQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetBrandsQuery, Result<List<BrandDto>>>
    {
        public async Task<Result<List<BrandDto>>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandscache= await cache.GetOrCreateAsync("brands", entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var brands= context.brands.Select(BrandMapper.ToDto)
                .ToListAsync(cancellationToken);
                return brands;
            });
            return brandscache;
        }
    }
}
