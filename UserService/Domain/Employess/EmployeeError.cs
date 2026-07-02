using Shared.Results;

namespace UserService.Domain.Employess
{
    public static class EmployeeError
    {
        public static Error DepartmentRequired =>
            Error.Validation(description: "Department is required.");
        public static Error JobTitleRequired =>
            Error.Validation(description: "Job title is required.");
        public static Error SalaryMustBeGreaterThanZero =>
            Error.Validation(description: "Salary must be greater than zero.");
        public static Error HireDateCannotBeInTheFuture =>
            Error.Validation(description: "Hire date cannot be in the future.");
    }
}
