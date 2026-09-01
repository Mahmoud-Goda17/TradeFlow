using SalesService.Application.Featuers.SalesOrdes.Dtos;
using SalesService.Application.Featuers.SalesOrdes.Interfaces;

namespace SalesService.Application.Featuers.SalesOrdes.services
{
    public class ProductServiceClient(HttpClient httpClientFactory) : IProductServiceClient
    {
        public Task<ProductInternalDto?> GetProductByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            return httpClientFactory.GetFromJsonAsync<ProductInternalDto>($"/api/Product/internal/{productId}", cancellationToken);
        }
    }
}
