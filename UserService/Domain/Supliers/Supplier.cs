using System;
using System.Security.Cryptography.X509Certificates;
using Shared.Commen;
using Shared.Results;
using UserService.Domain.Users;

namespace UserService.Domain.Supliers
{
    public sealed class Supplier : AuditableEntity
    {
        public Guid UserId { get; private set; }

        public string CompanyName { get; private set; } = default!;

        public string TaxNumber { get; private set; } = default!;

        public string BankAccount { get; private set; } = default!;

        public User User { get; set; } = default!;

        private Supplier() { }
        internal Supplier(Guid id, Guid userId, string companyName, string taxNumber, string bankAccount) : base(id)
        {
            UserId = userId;
            CompanyName = companyName;
            TaxNumber = taxNumber;
            BankAccount = bankAccount;
        }
        public static Result<Supplier> Create(Guid id, Guid userId, string companyName, string taxNumber, string bankAccount)
        {
            var errors = Errors(companyName, taxNumber, bankAccount);
            if (errors.Count > 0)
            {
                return errors;
            }
            var supplier = new Supplier(id, userId, companyName, taxNumber, bankAccount);
            return supplier;
        }
        public Result<Updated> Update(string companyName, string taxNumber, string bankAccount)
        {
            var errors = Errors(companyName, taxNumber, bankAccount);
            if (errors.Count > 0)
            {
                return errors;
            }
            CompanyName = companyName;
            TaxNumber = taxNumber;
            BankAccount = bankAccount;
            return Result.Updated;
        }
        private static List<Error> Errors(string companyName, string taxNumber, string bankAccount)
        {
            var errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(companyName))
            {
                errors.Add(SuplierError.CompanyNameRequired);
            }
            if (string.IsNullOrWhiteSpace(taxNumber))
            {
                errors.Add(SuplierError.TaxNumberRequired);
            }
            if (string.IsNullOrWhiteSpace(bankAccount))
            {
                errors.Add(SuplierError.BankAccountRequired);
            }
            return errors;
        }
    }
}
