using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Featuers.Categories.Dtos;
using ProductSercice.Application.Featuers.Categories.Mapper;
using ProductSercice.Application.Interfaces;
using ProductSercice.Domain.Category;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(IAppDbContext context,
        ILogger<CreateCategoryCommandHandler> logger, IMemoryCache cache) : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var name = request.name.Trim();
            var isexcist = await context.Categories.AnyAsync(b => b.Name == name);
            if (isexcist)
            {
                logger.LogWarning("Category with name {name} already exists", name);
                return ProductAppError.IsExcist("Category", "name", name);
            }
            var newCategory = Category.Create(Guid.NewGuid(), name, request.description);
            if (newCategory.IsError)
            {
                logger.LogWarning("Failed to create category: {Errors}", newCategory.Errors);
                return newCategory.Errors;
            }
            context.Categories.Add(newCategory.Value);
            await context.SaveChangesAsync(cancellationToken);
            cache.Remove("categories");
            return newCategory.Value.ToCategoryDto();
        }
    }
}
