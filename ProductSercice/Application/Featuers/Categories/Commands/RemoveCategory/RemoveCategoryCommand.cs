using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Categories.Commands.RemoveCategory
{
    public sealed record RemoveCategoryCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
