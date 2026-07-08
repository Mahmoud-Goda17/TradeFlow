using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Commands.RemoveBrand
{
    public class RemoveBrandCommandHandler(IAppDbContext context,
        ILogger<RemoveBrandCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveBrandCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await context.brands.FirstOrDefaultAsync(b => b.Id == request.id, cancellationToken);
            if (brand == null)
            {
                logger.LogWarning("Brand with id {id} not found", request.id);
                return ProductAppError.NotFound("Brand", request.id.ToString());
            }
            context.brands.Remove(brand);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("Brands");
            return Result.Deleted;
        }
    }
}
