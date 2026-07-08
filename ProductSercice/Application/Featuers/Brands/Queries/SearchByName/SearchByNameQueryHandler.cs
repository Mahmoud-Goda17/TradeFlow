using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Brands.Dtos;
using ProductSercice.Application.Featuers.Brands.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Queries.SearchByName
{
    public class SearchByNameQueryHandler(IAppDbContext context) : IRequestHandler<SearchByNameQuery, Result<List<BrandDto>>>
    {
        public async Task<Result<List<BrandDto>>> Handle(SearchByNameQuery request, CancellationToken cancellationToken)
        {
            var search =  request.name.Trim();
            var brands = await context.brands
                .Where(b => b.Name.Contains(search))
                .Select(BrandMapper.ToDto)
                .ToListAsync(cancellationToken);

            return brands;
        }
    }
}
