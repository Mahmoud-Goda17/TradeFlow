using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductSercice.Application.Featuers.Products.Dtos;
using ProductSercice.Application.Featuers.Products.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IAppDbContext context,
        ILogger<GetProductByIdQueryHandler> logger) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
    {
        public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product= await context.Products.Where(p=> p.Id == request.Id)
                .Select(ProductMapper.ToDto)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                logger.LogWarning("Product with id {Id} not found", request.Id);
                return ProductAppError.NotFound("Product", request.Id.ToString());
            }
            return product;
        }
    }
}
