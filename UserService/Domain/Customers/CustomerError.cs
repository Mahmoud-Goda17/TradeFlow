using Shared.Results;

namespace UserService.Domain.Customers
{
    public static class CustomerError
    {
        public static Error CompanyNameRequired =>
            Error.Validation(code: "Customer.CompanyNameRequire", description: "Company name is required.");
        public static Error TaxNumberRequired =>
            Error.Validation(code: "Customer.TaxNumberRequired", description:"Tax number is required.");
    }
}
