using InventoryService.Application.Featuers.stock.Dto;

namespace InventoryService.Application.Featuers.stock.Interfaces
{
    public interface IProductServiceClient
    {
        Task<ProductInternalDto?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken);
    }
}
