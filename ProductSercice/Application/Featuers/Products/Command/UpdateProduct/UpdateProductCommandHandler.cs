using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Interfaces;
using ProductSercice.Domain.Event;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler(IAppDbContext context,
        ILogger<UpdateProductCommandHandler> logger, IMemoryCache cache, IMediator mediator) : IRequestHandler<UpdateProductCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == request.id, cancellationToken);
            if (existingProduct == null)
            {
                logger.LogWarning("Product with id {Id} not found.", request.id);
                return ProductAppError.NotFound("product", request.id.ToString());
            }

            var exists = await context.Products.AnyAsync(
                p => p.Id != request.id &&
                    (p.SKU == request.sKU || p.Barcode == request.barcode),
                cancellationToken);
            if (exists)
            {
                logger.LogWarning("Product with SKU {SKU} or Barcode {Barcode} already exists.", request.sKU, request.barcode);
                return ProductAppError.IsExcist("product", "SKU", "Barcode", request.sKU, request.barcode);
            }
            var updateresult = existingProduct.Update(request.name,
                request.description, request.sKU,
                request.barcode, request.categoryId, request.brandId, request.costPrice, request.sellingPrice);
            if (updateresult.IsError)
            {
                logger.LogWarning("Failed to update product with id {Id}: {Errors}", request.id, updateresult.Errors);
                return updateresult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("products");
            var productEvent = new ProductEvent(
                eventId: Guid.NewGuid(),
                productId: existingProduct.Id,
                productName: existingProduct.Name);

            await mediator.Publish(productEvent,cancellationToken);
            return updateresult;
        }
    }
}
