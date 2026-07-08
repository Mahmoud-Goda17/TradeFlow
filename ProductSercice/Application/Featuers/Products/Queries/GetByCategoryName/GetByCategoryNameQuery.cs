using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByCategoryName
{
    public sealed record class GetByCategoryNameQuery(string categoryname): IRequest<Result<List<ProductDto>>>
    {
    }
}
