using Shared.Results;

namespace ProductSercice.Domain.Brand
{
    public static class BrandError
    {
        public static Error NameIsRequired => 
            Error.Validation("Brand.NameIsRequired", "Brand name is required.");
        public static Error DescriptionIsRequired => 
            Error.Validation("Brand.DescriptionIsRequired", "Brand description is required.");
        public static Error CountryIsRequired =>
            Error.Validation("Brand.CountryIsRequired", "Brand country is required.");
    }
}
