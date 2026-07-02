namespace UserService.Application.Featuers.Users.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = default!;

        public string TaxNumber { get; set; } = default!;

    }
}
