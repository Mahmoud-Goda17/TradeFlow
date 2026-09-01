using SalesService.Application.Featuers.SalesOrdes.Dtos;

namespace SalesService.Application.Featuers.SalesOrdes.Interfaces
{
    public interface ICustomerServiceClient
    {
        Task<CustomertInternalDto?> GetCustomerByIdAsync(Guid customerId, CancellationToken cancellationToken);
    }
}
