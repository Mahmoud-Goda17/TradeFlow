using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Categories.Dtos;
using ProductSercice.Application.Featuers.Categories.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Queries.SearchByName
{
    public class SearchByNameQueryHandler(IAppDbContext context,
        ILogger<SearchByNameQueryHandler> logger) : IRequestHandler<SearchByNameQuery, Result<List<CategoryDto>>>
    {
        public async Task<Result<List<CategoryDto>>> Handle(SearchByNameQuery request, CancellationToken cancellationToken)
        {
            var name = request.name.Trim();
            var categories = await context.Categories
                .Where(c => c.Name.Contains(name))
                .Select(CategoryMapper.Todto)
                .ToListAsync(cancellationToken);
            return categories;
        }
    }
}
