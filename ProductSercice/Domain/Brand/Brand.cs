using Shared.Commen;
using Shared.Results;

namespace ProductSercice.Domain.Brand
{
    public sealed class Brand : AuditableEntity
    {
        public string Name { get; private set; } = default!;

        public string Description { get; private set; }= default!;
        public string Country { get; private set; } = default!;
        private Brand() { }
        private Brand(Guid id, string name, string description, string country) : base(id)
        {
            Name = name;
            Description = description;
            Country = country;
        }
        public static Result<Brand> Create(Guid id, string name, string description, string country)
        {
            var errors = Errors(name, description, country);
            if (errors.Count > 0)
            {
                return errors;
            }
            var brand = new Brand(id, name, description, country);
            return brand;
        }
        public Result<Updated> Update(string name, string description, string country)
        {
            var errors = Errors(name, description, country);
            if (errors.Count > 0)
            {
                return errors;
            }
            Name = name;
            Description = description;
            Country = country;
            return Result.Updated;
        }
        private static List<Error> Errors(string name, string description, string country)
        {
            var errors = new List<Error>();
            if (string.IsNullOrEmpty(name))
            {
                errors.Add(BrandError.NameIsRequired);
            }
            if (string.IsNullOrEmpty(description))
            {
                errors.Add(BrandError.DescriptionIsRequired);
            }
            if (string.IsNullOrEmpty(country))
            {
                errors.Add(BrandError.CountryIsRequired);
            }
            return errors;
        }
    }
}
