using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommandHandler(IAppDbContext context,
        ILogger<RemoveCategoryCommandHandler> logger, IMemoryCache cache) : IRequestHandler<RemoveCategoryCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c=>c.Id==request.id,cancellationToken);
            if (category == null)
            {
                logger.LogWarning("Category with id {id} not found", request.id);
                return ProductAppError.NotFound("Category", request.id.ToString());
            }
            context.Categories.Remove(category);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("Categories");
            return Result.Deleted;
        }
    }
}
