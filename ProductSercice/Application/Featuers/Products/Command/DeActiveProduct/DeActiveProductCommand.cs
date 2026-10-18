using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.DeActiveProduct
{
    public sealed record DeActiveProductCommand(Guid id) : IRequest<Result<Updated>>
    {
    }
}
