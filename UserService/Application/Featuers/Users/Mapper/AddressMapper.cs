using UserService.Application.Featuers.Users.Dtos;
using UserService.Domain.Addresses;

namespace UserService.Application.Featuers.Users.Mapper
{
    public static class AddressMapper
    {
        public static AddressDto ToDto(this Address address)
        {
            return new AddressDto
            {
                Street = address.Street,
                BuildingNo = address.BuildingNo,
                City = address.City,
            }; 
        }

        public static List<AddressDto> ToDtos(this List<Address> addresses)
            {
                return addresses.Select(a => a.ToDto()).ToList();
            }
    }
}
