namespace UserService.Application.Featuers.Users.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Department { get;  set; } = default!;

        public string JobTitle { get;  set; } = default!;

        public decimal Salary { get;  set; }

        public DateTimeOffset HireDate { get;  set; }

    }
}
