using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.RemoveProduct
{
    public sealed record RemoveProductCommand(Guid id):IRequest<Result<Deleted>>
    {
    }
}
