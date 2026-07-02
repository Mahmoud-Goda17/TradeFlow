using Shared.Commen;
using UserService.Domain.Customers;
using UserService.Domain.Employess;
using UserService.Domain.Supliers;
using UserService.Domain.Addresses;
using Shared.Results;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net;

namespace UserService.Domain.Users
{
    public sealed class User : AuditableEntity
    {
        public string FullName { get; private set; } = default!;

        public string Email { get; private set; } = default!;

        public string Phone { get; private set; } = default!;

        public UserType UserType { get; private set; }

        public bool IsActive { get; private set; }

        public Empioyee? Employee { get; private set; }

        public Customer? Customer { get; private set; }

        public Supplier? Supplier { get; private set; }

        private readonly List<Address> _addresses = [];
        public IEnumerable<Address> Addresses => _addresses.AsReadOnly();

        private User() { }
        private User(Guid id, string fullName, string email, string phone, UserType userType, bool isActive) : base(id)
        {
            FullName = fullName;
            Email = email;
            Phone = phone;
            UserType = userType;
            IsActive = isActive;
        }
        public static Result<User> Create(Guid id, string fullName, string email, string phone, UserType userType, bool isActive)
        {
            var errors = Errors(fullName, email, phone, userType, isActive);
            if (errors.Count > 0)
            {
                return errors;
            }
            var user = new User(id, fullName, email, phone, userType, isActive);
            return user;
        }
        public Result<Updated> Update(string fullName, string email, string phone, UserType userType, bool isActive)
        {
            var errors = Errors(fullName, email, phone, userType, isActive);
            if (errors.Count > 0)
            {
                return errors;
            }
            FullName = fullName;
            Email = email;
            Phone = phone;
            UserType = userType;
            IsActive = isActive;
            return Result.Updated;
        }
        public Result<Updated> BeActive()
        {
            if (IsActive)
            {
                return UserError.UserAlreadyActive;
            }
            IsActive = true;
            return Result.Updated;
        }
        public Result<Updated> BeNotActive()
        {
            if (!IsActive)
            {
                return UserError.UserAlreadyNotActive;
            }
            IsActive = false;
            return Result.Updated;
        }
        public Result<Empioyee> AddEmployee(Guid id, string department, string jobTitle, decimal salary, DateTimeOffset hireDate)
        {
            if (UserType != UserType.Employee)
            {
                return UserError.UserTypeMustBeEmployee;
            }
            if (Employee != null)
            {
                return UserError.EmployeeAlreadyExists;
            }
            var employee = Empioyee.Create(id, Id, department, jobTitle, salary, hireDate);
            if (employee.IsError)
            {
                return employee.Errors;
            }

            return employee.Value;
        }
        public Result<Updated> UpdateEmployee(string department, string jobTitle, decimal salary, DateTimeOffset hireDate)
        {
            if (Employee == null)
            {
                return UserError.EmployeeIsNotExists;
            }
         var employee= Employee.Update(department, jobTitle, salary, hireDate);
            if (employee.IsError)
            {
                return employee.Errors;
            }


            return employee.Value;

        }
        public Result<Customer> AddCustomer(Guid id, string companyName, string taxNumber)
        {
            if (UserType != UserType.Customer)
            {
                return UserError.UserTypeMustBeCustomer;
            }
            if (Customer != null)
            {
                return UserError.CustomerAlreadyExists;
            }
            var customer = Customer.Create(id, Id, companyName, taxNumber);
            if (customer.IsError)
            {
                return customer.Errors;
            }

           return customer.Value;

        }
        public Result<Updated> UpdateCustomer(string companyName, string taxNumber)
        {
            if (Customer == null)
            {
                return UserError.CustomerIsNotExists;
            }
           var customer= Customer.Update(companyName, taxNumber);
            if (customer.IsError)
            {
                return customer.Errors;
            }

            return customer.Value;
        }
        public Result<Supplier> AddSupplier(Guid id, string companyName, string taxNumber, string bankAccount)
        {
            if (UserType != UserType.Supplier)
            {
                return UserError.UserTypeMustBeSupplier;
            }
            if (Supplier != null)
            {
                return UserError.SupplierAlreadyExists;
            }
            var supplier = Supplier.Create(id, Id, companyName, taxNumber, bankAccount);
            if (supplier.IsError)
            {
                return supplier.Errors;
            }

            return supplier.Value;
        }
        public Result<Updated> UpdateSupplier(string companyName, string taxNumber, string bankAccount)
        {
            if (Supplier == null)
            {
                return UserError.SupplierIsNotExists;
            }
            var supplier= Supplier.Update(companyName, taxNumber, bankAccount);
            if (supplier.IsError)
            {
                return supplier.Errors;
            }
            return Result.Updated;
        }
        public Result<Address> AddAddress(Guid id, string city, string street, string buildingNo, string? postalCode)
        {
            var address = Address.Create(id, Id, city, street, buildingNo, postalCode);

            if (address.IsError)
            {
                return address.Errors;
            }
            _addresses.Add(address.Value);
            return address.Value;
        }
        public Result<Updated> UpdateAdreeses(List<AddressData> addresses)
        {
            _addresses.RemoveAll(existing => addresses.All(v => v.Id != existing.Id));
            foreach (var address in addresses)
            {
                var existingAddress = _addresses.FirstOrDefault(a => a.Id == address.Id);
                if (existingAddress != null)
                {
                    var updateResult = existingAddress.Update(address.City, address.Street, address.BuildingNo, address.PostalCode);
                    if (updateResult.IsError)
                    {
                        return updateResult.Errors;
                    }
                }
                else
                {
                    var addResult = AddAddress(Guid.NewGuid(),address.City, address.Street, address.BuildingNo, address.PostalCode);
                    if (addResult.IsError)
                    {
                        return addResult.Errors;
                    }
                }
            }
            return Result.Updated;
        }
        public Result<Deleted> DeleteAddress()
        {
            if (_addresses.Count == 0)
            {
                return UserError.NoAddressesToDelete;
            }
            _addresses.Clear();
            return Result.Deleted;
        }
        private static List<Error> Errors(string fullName, string email, string phone, UserType userType, bool isActive)
        {
            var errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                errors.Add(UserError.FullNameRequired);
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(UserError.EmailRequired);
            }
            try
            {
                var _ = new MailAddress(email);
            }
            catch
            {
                errors.Add(UserError.InvalidEmailFormat);
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                errors.Add(UserError.PhoneRequired);
            }
            if (!Regex.IsMatch(phone, @"^\+?\d{7,15}$"))
            {
                errors.Add(UserError.InvalidPhoneFormat);
            }
            if (!Enum.IsDefined(typeof(UserType), userType))
            {
                errors.Add(UserError.InvalidUserType);
            }
            return errors;
        }
    }
}
