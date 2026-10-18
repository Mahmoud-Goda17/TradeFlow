using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Brands.Commands.UpdateBrand
{
    public sealed record  UpdateBrandCommand(Guid id,string name, string description, string country) : IRequest<Result<Updated>>
    {
    }
}
