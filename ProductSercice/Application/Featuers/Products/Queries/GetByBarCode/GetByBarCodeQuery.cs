using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.GetByBarCode
{
    public sealed record class GetByBarCodeQuery(string barcode) : IRequest<Result<ProductDto>>
    {
    }
}
