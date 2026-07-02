namespace UserService.Application.Featuers.Users.Dtos
{
    public class SupplierDto
    {
        public Guid Id { get; set; }
        public string CompanyName { get;  set; } = default!;

        public string TaxNumber { get;  set; } = default!;

        public string BankAccount { get;  set; } = default!;

    }
}
