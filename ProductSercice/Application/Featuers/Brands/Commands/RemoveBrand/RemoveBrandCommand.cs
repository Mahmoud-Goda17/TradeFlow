using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Commands.RemoveBrand
{
    public sealed record  RemoveBrandCommand(Guid id) : IRequest<Result<Deleted>>
    {
    }
}
