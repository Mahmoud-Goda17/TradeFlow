using MediatR;
using ProductSercice.Application.Featuers.Categories.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Queries.GetCategories
{
    public sealed record class GetCategoriesQuery:IRequest<Result<List<CategoryDto>>>
    {
    }
}
