using UserService.Application.Featuers.Users.Dtos;
using UserService.Domain.Employess;

namespace UserService.Application.Featuers.Users.Mapper
{
    public static class EmployeeMapper
    {
        public static EmployeeDto Todto(this Empioyee employee)
        {
            ArgumentNullException.ThrowIfNull(employee);
            return new EmployeeDto
            {
                Id = employee.Id,
                Department = employee.Department,
                HireDate = employee.HireDate,
                JobTitle = employee.JobTitle,
                Salary = employee.Salary,
            };
        }
        public static List<EmployeeDto> todtolist( this IEnumerable<Empioyee> employees)
        {
           return employees.Select(e=>e.Todto()).ToList();
        }
    }
}
