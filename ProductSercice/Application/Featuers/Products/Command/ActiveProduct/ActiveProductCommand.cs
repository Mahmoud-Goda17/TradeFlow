using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.ActiveProduct
{
    public sealed record ActiveProductCommand(Guid id) : IRequest<Result<Updated>>
    {
    }
}
