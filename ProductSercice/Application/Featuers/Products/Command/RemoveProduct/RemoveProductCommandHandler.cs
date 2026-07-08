using System.Threading;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.RemoveProduct
{
    public class RemoveProductCommandHandler(IAppDbContext context,
        ILogger<RemoveProductCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveProductCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await context.Products.FirstOrDefaultAsync(p => p.Id == request.id, cancellationToken);
            if (existingProduct == null)
            {
                logger.LogWarning("Product with id {Id} not found.", request.id);
                return ProductAppError.NotFound("product", request.id.ToString());
            }
            context.Products.Remove(existingProduct);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("products");
            return Result.Deleted;
        }
    }
}
