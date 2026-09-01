using SalesService.Application.Featuers.SalesOrdes.Dtos;

namespace SalesService.Application.Featuers.SalesOrdes.Interfaces
{
    public interface IProductServiceClient
    {
        Task<ProductInternalDto?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    }
}
