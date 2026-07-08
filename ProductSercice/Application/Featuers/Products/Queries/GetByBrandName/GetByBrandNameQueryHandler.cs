using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByBrandName
{
    public class GetByBrandNameQueryHandler(IAppDbContext context) : IRequestHandler<GetByBrandNameQuery, Result<List<ProductDto>>>
    {
        public async Task<Result<List<ProductDto>>> Handle(GetByBrandNameQuery request, CancellationToken cancellationToken)
        {
            var brandname= request.brandname.Trim();
            var products=await context.Products.Where(p=>p.Brand.Name == brandname)
                .Select(ProductMapper.ToDto)
                .ToListAsync(cancellationToken);
            return products;
        }
    }
}
