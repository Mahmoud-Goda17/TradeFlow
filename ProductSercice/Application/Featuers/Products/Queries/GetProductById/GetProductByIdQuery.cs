using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetProductById
{
    public sealed record class GetProductByIdQuery(Guid Id) : IRequest<Result<ProductDto>>
    {
    }
}
