using Shared.Commen;
using Shared.Results;
using UserService.Domain.Users;

namespace UserService.Domain.Customers
{
    public sealed class Customer:AuditableEntity
    {
        public Guid UserId { get; private set; }

        public string CompanyName { get; private set; } = default!;

        public string TaxNumber { get; private set; } = default!;
        public User User { get; set; } = default!;

        private Customer() { }
        internal Customer(Guid id, Guid userId, string companyName, string taxNumber) : base(id)
        {
            UserId = userId;
            CompanyName = companyName;
            TaxNumber = taxNumber;
        }
        public static Result<Customer> Create(Guid id, Guid userId, string companyName, string taxNumber)
        {
            var errors = Errors(companyName, taxNumber);
            if (errors.Count > 0)
            {
                return errors;
            }
            var customer = new Customer(id, userId, companyName, taxNumber);
            return customer;
        }
        public Result<Updated> Update(string companyName, string taxNumber)
        {
            var errors = Errors(companyName, taxNumber);
            if (errors.Count > 0)
            {
                return errors;
            }
            CompanyName = companyName;
            TaxNumber = taxNumber;
            return Result.Updated;
        }
        private static List<Error> Errors(string companyName, string taxNumber)
        {
            var errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(companyName))
            {
                errors.Add(CustomerError.CompanyNameRequired);
            }
            if (string.IsNullOrWhiteSpace(taxNumber))
            {
                errors.Add(CustomerError.TaxNumberRequired);
            }
            return errors;
        }
    }
}
