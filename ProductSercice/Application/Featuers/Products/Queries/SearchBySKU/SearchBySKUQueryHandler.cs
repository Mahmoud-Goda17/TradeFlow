using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.SearchBySKU
{
    public class SearchBySKUQueryHandler(IAppDbContext context, ILogger<SearchBySKUQueryHandler> logger) : IRequestHandler<SearchBySKUQuery, Result<ProductDto>>
    {
        public async Task<Result<ProductDto>> Handle(SearchBySKUQuery request, CancellationToken cancellationToken)
        {
            var product = await context.Products.Where(p => p.SKU == request.SKU)
                .Select(ProductMapper.ToDto)
                .FirstOrDefaultAsync(cancellationToken);
            if (product == null)
            {
                logger.LogWarning("Product with SKU {SKU} not found", request.SKU);
                return ProductAppError.NotFound("Product", request.SKU);
            }
            return product;
        }
    }
}
