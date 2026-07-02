using UserService.Application.Featuers.Users.Dtos;
using UserService.Domain.Customers;

namespace UserService.Application.Featuers.Users.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(this Customer customer)
        {
            ArgumentNullException.ThrowIfNull(customer);
            return new CustomerDto
            {
                Id = customer.Id,
                CompanyName = customer.CompanyName,
                TaxNumber = customer.TaxNumber,
            };
        }
        public static List<CustomerDto> ToDtoList(this IEnumerable<Customer> customers)
        {
            return customers.Select(c => c.ToDto()).ToList();
        }
    }
}
