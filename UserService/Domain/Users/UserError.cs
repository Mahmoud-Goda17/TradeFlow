using Shared.Results;

namespace UserService.Domain.Users
{
    public static class UserError
    {
        public static Error FullNameRequired =>
            Error.Validation(code: "User.FullNameRequired", description: "Full name is required.");
        public static Error EmailRequired =>
            Error.Validation(code: "User.EmailRequired", description: "Email is required.");
        public static Error InvalidEmailFormat =>
            Error.Validation(code: "User.InvalidEmailFormat", description: "Invalid email format.");
        public static Error PhoneRequired =>
            Error.Validation(code: "User.PhoneRequired", description: "Phone is required.");
        public static Error InvalidPhoneFormat =>
            Error.Validation(code: "User.InvalidPhoneFormat", description: "Invalid phone format. It should be a valid international phone number.");
        public static Error InvalidUserType =>
            Error.Validation(code: "User.InvalidUserType", description: "User type is not true.");
        public static Error UserAlreadyActive =>
            Error.Conflict(code: "User.UserAlreadyActive", description: "User is already active.");
        public static Error UserAlreadyNotActive =>
            Error.Conflict(code: "User.UserAlreadyNotActive", description: "User is already not active.");
        public static Error UserTypeMustBeEmployee =>
            Error.Validation(code: "User.UserTypeMustBeEmployee", description: "User type must be employee to add employee details.");
        public static Error EmployeeAlreadyExists =>
            Error.Conflict(code: "User.EmployeeAlreadyExists", description: "Employee details is exists for this user.");
        public static Error EmployeeIsNotExists =>
            Error.NotFound(code: "User.EmployeeIsNotExists", description: "Employee details is  exists for this user.");
        public static Error UserTypeMustBeCustomer =>
           Error.Validation(code: "User.UserTypeMustBeCustomer", description: "User type must be Customer to add Customer details.");
        public static Error CustomerAlreadyExists =>
          Error.Conflict(code: "User.CustomerAlreadyExists", description: "Cuatomer details is exists for  this user.");
        public static Error CustomerIsNotExists =>
            Error.NotFound(code: "User.CustomerIsNotExists", description: "Customer  is not exists for this user.");
        public static Error SupplierAlreadyExists =>
          Error.Conflict(code: "User.SupplierAlreadyExists", description: "Supplierr details is  exists for this user.");
        public static Error UserTypeMustBeSupplier =>
          Error.Validation(code: "User.UserTypeMustBeSupplie", description: "User type must be Supplier to add Supplier details.");
        public static Error SupplierIsNotExists =>
              Error.NotFound(code: "User.SupplierIsNotExists ", description: "Suplier  is not exists for this user.");
        public static Error NoAddressesToDelete =>
            Error.Validation(code: "User.UserHasNoAddresses", description: "This use has not any address.");



    }
}
