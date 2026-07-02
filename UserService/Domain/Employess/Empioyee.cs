using Shared.Commen;
using Shared.Results;
using UserService.Domain.Users;
namespace UserService.Domain.Employess
{
    public sealed class Empioyee : AuditableEntity
    {

        public Guid UserId { get; set; }

        public string Department { get; private set; } = default!;

        public string JobTitle { get; private set; } = default!;

        public decimal Salary { get; private set; }

        public DateTimeOffset HireDate { get; private set; }

        public User User { get; set; } = default!;

        private Empioyee() { }
        internal Empioyee(Guid id, Guid userId, string department, string jobTitle, decimal salary, DateTimeOffset hireDate) : base(id)
        {
            UserId = userId;
            Department = department;
            JobTitle = jobTitle;
            Salary = salary;
            HireDate = hireDate;
        }
        public static Result<Empioyee> Create(Guid id, Guid userId, string department, string jobTitle, decimal salary, DateTimeOffset hireDate)
        {
            var errors = Errors(department, jobTitle, salary, hireDate);
            if (errors.Count > 0)
            {
                return errors;
            }
            var employee = new Empioyee(id, userId, department, jobTitle, salary, hireDate);
            return employee;
        }
        public Result<Updated> Update(string department, string jobTitle, decimal salary, DateTimeOffset hireDate)
        {
            var errors = Errors(department, jobTitle, salary, hireDate);
            if (errors.Count > 0)
            {
                return errors;
            }
            Department = department;
            JobTitle = jobTitle;
            Salary = salary;
            HireDate = hireDate;
            return Result.Updated;
        }
        public static List<Error> Errors(string department, string jobTitle, decimal salary, DateTimeOffset hireDate)
        {
            var errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(department))
            {
                errors.Add(EmployeeError.DepartmentRequired);
            }
            if (string.IsNullOrWhiteSpace(jobTitle))
            {
                errors.Add(EmployeeError.JobTitleRequired);
            }
            if (salary <= 0)
            {
                errors.Add(EmployeeError.SalaryMustBeGreaterThanZero);
            }
            if (hireDate > DateTimeOffset.UtcNow)
            {
                errors.Add(EmployeeError.HireDateCannotBeInTheFuture);
            }
            return errors;
        }
    }
}
