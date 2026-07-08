using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.SearchByName
{
    public class SearchByNameQueryHandler(IAppDbContext context
        ) : IRequestHandler<SearchByNameQuery, Result<List<ProductDto>>>
    {
        public async Task<Result<List<ProductDto>>> Handle(SearchByNameQuery request, CancellationToken cancellationToken)
        {
            var searchname = request.Name.Trim();
            var products = await context.Products.Where(p => p.Name.Contains(searchname))
                .Select(ProductMapper.ToDto)
                .ToListAsync(cancellationToken);
            return products;
        }
    }
}
