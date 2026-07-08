using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Featuers.Categories.Dtos;
using ProductSercice.Application.Featuers.Categories.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(IAppDbContext context,
        IMemoryCache cache) : IRequestHandler<GetCategoriesQuery, Result<List<CategoryDto>>>
    {
        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoriesCache = await cache.GetOrCreateAsync("categories", async entry =>
            {
                entry.Size = 1;
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var categories = await context.Categories.Select(CategoryMapper.Todto)
                .ToListAsync(cancellationToken);
                return categories;
            });
            return categoriesCache;
        }
    }
}
