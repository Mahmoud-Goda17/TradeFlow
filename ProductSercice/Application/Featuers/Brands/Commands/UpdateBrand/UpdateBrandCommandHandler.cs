using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Commands.UpdateBrand
{
    public class UpdateBrandCommandHandler(IAppDbContext context,
        ILogger<UpdateBrandCommandHandler> logger, IMemoryCache cache) : IRequestHandler<UpdateBrandCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await context.brands.FirstOrDefaultAsync(b => b.Id == request.id, cancellationToken);
            if (brand == null)
            {
                logger.LogWarning("Brand with id {id} not found", request.id);
                return ProductAppError.NotFound("Brand", request.id.ToString());
            }
            var name = request.name.Trim();
            var isExcist = await context.brands.AnyAsync(b => b.Id != request.id && b.Name == name, cancellationToken);
            if (isExcist)
            {
                logger.LogWarning("Brand with name {name} already exists", name);
                return ProductAppError.IsExcist("Brand", "name", name);
            }
            var updateresult = brand.Update(name, request.description, request.country);
            if (updateresult.IsError)
            {
                logger.LogWarning("Failed to update brand with id {Id}: {Errors}", request.id, updateresult.Errors);
                return updateresult.Errors;
            }
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("Brands");
            return updateresult;
        }
    }
}
