using Shared.Results;

namespace UserService.Domain.Addresses
{
    public static class AddressError
    {
        public static Error CityRequired=>
            Error.Validation(code: "Address.CityRequired", description: "City is required.");
        public static Error streetRequired =>
            Error.Validation(code: "Address.streetRequired", description: "street is required.");
        public static Error buildingNoRequired =>
            Error.Validation(code: "Address.buildingNoRequired", description: "street is required.");

    }
}
