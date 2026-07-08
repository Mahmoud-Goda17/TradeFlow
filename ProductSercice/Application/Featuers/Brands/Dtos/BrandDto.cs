namespace ProductSercice.Application.Featuers.Brands.Dtos
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get;  set; } = default!;

        public string Description { get;  set; } = default!;
        public string Country { get; set; } = default!;

    }
}
