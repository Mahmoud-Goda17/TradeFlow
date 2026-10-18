using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Commands.UpdateCategory
{
    public sealed record  UpdateCategoryCommand(Guid id,string name, string description) : IRequest<Result<Updated>>
    {
    }
}
