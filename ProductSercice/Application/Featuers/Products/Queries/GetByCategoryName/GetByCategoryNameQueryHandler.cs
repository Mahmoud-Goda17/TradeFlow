using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByCategoryName
{
    public class GetByCategoryNameQueryHandler(IAppDbContext context) : IRequestHandler<GetByCategoryNameQuery, Result<List<ProductDto>>>
    {
        public async Task<Result<List<ProductDto>>> Handle(GetByCategoryNameQuery request, CancellationToken cancellationToken)
        {
            var categoryname =  request.categoryname.Trim();
            var products = await context.Products.Where(p => p.Category.Name == categoryname)
                .Select(ProductMapper.ToDto)
                .ToListAsync(cancellationToken);
            return products;

        }
    }
}
