using SalesService.Application.Featuers.SalesOrdes.Dtos;
using SalesService.Application.Featuers.SalesOrdes.Interfaces;

namespace SalesService.Application.Featuers.SalesOrdes.services
{
    public class CustomerServiceClient(HttpClient httpClientFactory) : ICustomerServiceClient
    {
        public Task<CustomertInternalDto?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            return httpClientFactory.GetFromJsonAsync<CustomertInternalDto>($"/api/Customer/internal/{customerId}", cancellationToken);

        }
    }
}
