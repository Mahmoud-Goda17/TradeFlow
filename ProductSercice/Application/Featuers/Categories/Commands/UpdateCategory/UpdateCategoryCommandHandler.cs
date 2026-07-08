using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(IAppDbContext context,
        ILogger<UpdateCategoryCommandHandler> logger, IMemoryCache cache) : IRequestHandler<UpdateCategoryCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken);
            if (category == null)
            {
                logger.LogWarning("Category with id {id} not found", request.id);
                return ProductAppError.NotFound("Category", request.id.ToString());
            }
            var name = request.name.Trim();
            var isExcist = await context.Categories.AnyAsync(c => c.Id != request.id && c.Name == name, cancellationToken);
            if (isExcist)
            {
                logger.LogWarning("Category with name {name} already exists", name);
                return ProductAppError.IsExcist("Category", "name", name);
            }
            var updateresult = category.Update(name, request.description);
            if (updateresult.IsError)
            {
                logger.LogWarning("Failed to update category with id {Id}: {Errors}", request.id, updateresult.Errors);
                return updateresult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("Categories");
            return updateresult;
        }
    }
}
