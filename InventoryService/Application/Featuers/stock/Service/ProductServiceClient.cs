using InventoryService.Application.Featuers.stock.Dto;
using InventoryService.Application.Featuers.stock.Interfaces;

namespace InventoryService.Application.Featuers.stock.Service
{
    public class ProductServiceClient(HttpClient httpClientFactory) : IProductServiceClient
    {
        public Task<ProductInternalDto?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return httpClientFactory.GetFromJsonAsync<ProductInternalDto>($"/api/Product/internal/{productId}", cancellationToken);
        }
    }
}
