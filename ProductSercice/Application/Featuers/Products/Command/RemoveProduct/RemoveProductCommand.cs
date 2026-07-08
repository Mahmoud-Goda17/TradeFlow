using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.RemoveProduct
{
    public sealed record class RemoveProductCommand(Guid id):IRequest<Result<Deleted>>
    {
    }
}
