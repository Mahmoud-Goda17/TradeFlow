using Shared.Commen;
using Shared.Results;
using UserService.Domain.Users;

namespace UserService.Domain.Addresses
{
    public sealed class Address : AuditableEntity
    {
        public Guid UserId { get; private set; }

        public string City { get; private set; } = default!;

        public string Street { get; private set; } = default!;

        public string BuildingNo { get; private set; } = default!;

        public string? PostalCode { get; private set; }

        public User User { get; set; } = default!;

        private Address() { }
        internal Address(Guid id, Guid userId, string city, string street, string buildingNo, string? postalCode) : base(id)
        {
            UserId = userId;
            City = city;
            Street = street;
            BuildingNo = buildingNo;
            PostalCode = postalCode;
        }
        public static Result<Address> Create(Guid id, Guid userId, string city, string street, string buildingNo, string? postalCode)
        {
            var errors = Errors(city, street, buildingNo);
            if (errors.Count > 0)
            {
                return errors;
            }
            var address = new Address(id, userId, city, street, buildingNo, postalCode);
            return address;
        }
        public Result<Updated> Update(string city, string street, string buildingNo, string? postalCode)
        {
            var errors = Errors(city, street, buildingNo);
            if (errors.Count > 0)
            {
                return errors;
            }
            City = city;
            Street = street;
            BuildingNo = buildingNo;
            PostalCode = postalCode;
            return Result.Updated;
        }
        private static List<Error> Errors(string city, string street, string buildingNo)
        {
            var errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(city))
            {
                errors.Add(AddressError.CityRequired);
            }
            if (string.IsNullOrWhiteSpace(street))
            {
                errors.Add(AddressError.streetRequired);
            }
            if (string.IsNullOrWhiteSpace(buildingNo))
            {
                errors.Add(AddressError.buildingNoRequired);
            }
            return errors;
        }
    }
}
