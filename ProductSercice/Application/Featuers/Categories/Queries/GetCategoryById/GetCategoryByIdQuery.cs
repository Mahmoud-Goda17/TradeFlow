using MediatR;
using ProductSercice.Application.Featuers.Categories.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Queries.GetCategoryById
{
    public sealed record  GetCategoryByIdQuery(Guid id) : IRequest<Result<CategoryDto>>
    {
    }
}
