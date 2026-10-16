using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Interfaces;
using ProductSercice.Domain.Event;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.ActiveProduct
{
    public class ActiveProductCommandHandler(IAppDbContext context,
        ILogger<ActiveProductCommandHandler> logger, IMemoryCache cache,IMediator mediator) : IRequestHandler<ActiveProductCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(ActiveProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == request.id, cancellationToken);
            if (existingProduct == null)
            {
                logger.LogWarning("Product with id {Id} not found.", request.id);
                return ProductAppError.NotFound("product", request.id.ToString());
            }
            var result = existingProduct.Activate();
            if (result.IsError)
            {
                logger.LogWarning("Failed to activate product with id {Id}: {Errors}", request.id, result.Errors);
                return result.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("products");
            var productEvent = new ProductEvent(
                            eventId: Guid.NewGuid(),
                            productId: existingProduct.Id,
                            productName: existingProduct.Name);

            await mediator.Publish(productEvent,cancellationToken);
            return result;

        }
    }
}
