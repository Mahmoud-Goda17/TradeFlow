using UserService.Application.Featuers.Users.Dtos;
using UserService.Domain.Supliers;

namespace UserService.Application.Featuers.Users.Mapper
{
    public static class SupplierMapper
    {
            public static SupplierDto ToDto(this Supplier supplier)
            {
                ArgumentNullException.ThrowIfNull(supplier);
                return new SupplierDto
                {
                    Id = supplier.Id,
                    BankAccount = supplier.BankAccount,
                    CompanyName = supplier.CompanyName,
                    TaxNumber = supplier.TaxNumber,
                };
            }
            public static List<SupplierDto> ToDtoList(this IEnumerable<Supplier> suppliers)
            {
                return suppliers.Select(c => c.ToDto()).ToList();
            }
    }
}
