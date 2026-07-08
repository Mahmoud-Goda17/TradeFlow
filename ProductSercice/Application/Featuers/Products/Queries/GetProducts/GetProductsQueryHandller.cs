using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetProducts
{
    public class GetProductsQueryHandller(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
    {
        public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var productsCache = await cache.GetOrCreateAsync("products", entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var products = context.Products.Select(ProductMapper.ToDto).
                    ToListAsync(cancellationToken);
                return products;
            });
            return productsCache;

        }
    }
}
