using System.Linq.Expressions;
using ProductSercice.Application.Featuers.Categories.Dtos;
using ProductSercice.Domain.Category;

namespace ProductSercice.Application.Featuers.Categories.Mapper
{
    public static class CategoryMapper
    {
        public static readonly Expression<Func<Category, CategoryDto>> Todto =
            c => new CategoryDto
            {
                Id = c.Id,
                Description = c.Description,
                Name = c.Name,
            };

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Description = category.Description,
                Id = category.Id,
                Name = category.Name,
            };
        }
        
            
        
    }
}
