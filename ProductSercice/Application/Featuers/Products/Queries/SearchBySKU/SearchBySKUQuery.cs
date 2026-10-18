using MediatR;
using ProductSercice.Application.Featuers.Products.Dtos;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Queries.SearchBySKU
{
    public sealed record  SearchBySKUQuery(string SKU) : IRequest<Result<ProductDto>>    
    {
    }
}
