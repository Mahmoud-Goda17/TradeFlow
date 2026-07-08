using MediatR;
using ProductSercice.Application.Featuers.Categories.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Commands.CreateCategory
{
    public sealed record class CreateCategoryCommand(string name, string description) : IRequest<Result<CategoryDto>>
    {
    }
}
