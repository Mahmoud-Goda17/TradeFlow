using Shared.Results;

namespace UserService.Domain.Supliers
{
    public static class SuplierError
    {
        public static Error CompanyNameRequired =>
            Error.Validation(code: "Suplier.CompanyNameRequire", description: "Company name is required.");
        public static Error TaxNumberRequired =>
            Error.Validation(code: "Suplier.TaxNumberRequired", description: "Tax number is required.");
        public static Error BankAccountRequired=>
            Error.Validation(code: "Suplier.BankAccountRequired", description: "Bank account is required.");
    }
}
