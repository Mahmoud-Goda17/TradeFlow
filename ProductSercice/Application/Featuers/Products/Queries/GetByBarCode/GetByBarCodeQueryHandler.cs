using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByBarCode
{
    public class GetByBarCodeQueryHandler(IAppDbContext context,
        ILogger<GetByBarCodeQueryHandler> logger) : IRequestHandler<GetByBarCodeQuery, Result<ProductDto>>
    {
        public async Task<Result<ProductDto>> Handle(GetByBarCodeQuery request, CancellationToken cancellationToken)
        {
            var product = await context.Products.Where(p => p.Barcode == request.barcode)
                .Select(ProductMapper.ToDto)
                .FirstOrDefaultAsync(cancellationToken);
            if (product == null)
            {
                logger.LogWarning("Product with barcode {barcode} not found", request.barcode);
                return ProductAppError.NotFound("Product", request.barcode);
            }
            return product;
        }
    }
}
