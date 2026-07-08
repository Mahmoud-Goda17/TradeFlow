using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductSercice.Application.Featuers.Categories.Dtos;
using ProductSercice.Application.Featuers.Categories.Mapper;
using ProductSercice.Application.Interfaces;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(IAppDbContext context,
        ILogger<GetCategoryByIdQueryHandler> logger) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.Where(c => c.Id == request.id)
                .Select(CategoryMapper.Todto)
                .FirstOrDefaultAsync(cancellationToken);
            if (category == null)
            {
                logger.LogWarning("Category with id {Id} not found", request.id);
                return ProductAppError.NotFound("Category", request.id.ToString());
            }
            return category;
        }
    }
}
