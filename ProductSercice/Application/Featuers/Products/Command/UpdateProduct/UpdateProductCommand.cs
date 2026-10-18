using MediatR;
using Shared.Results;

namespace ProductSercice.Application.Featuers.Products.Command.UpdateProduct
{
    public sealed record UpdateProductCommand(Guid id, string name,string description,string sKU,
        string barcode, Guid categoryId, Guid brandId, decimal costPrice, decimal sellingPrice) : IRequest<Result<Updated>>
    {
    }
}
