using ProductSercice.Domain.Brand;
using Shared.Commen;
using Shared.Results;

namespace ProductSercice.Domain.Category
{
    public sealed class Category:AuditableEntity
    {
        public string Name { get; private set; } = default!;

        public string Description { get; private set; } = default!;
        private Category() { }
        private Category(Guid id, string name, string description) : base(id)
        {
            Name = name;
            Description = description;
        }
        public static Result<Category> Create(Guid id, string name, string description)
        {
            var errors = Errors(name, description);
            if (errors.Count > 0)
            {
                return errors;
            }
            var category = new Category(id, name, description);
            return category;
        }
        public Result<Updated> Update(string name, string description)
        {
            var errors = Errors(name, description);
            if (errors.Count > 0)
            {
                return errors;
            }
            Name = name;
            Description = description;
            return Result.Updated;
        }
        private static List<Error> Errors(string name, string description)
        {
            var errors = new List<Error>();
            if (string.IsNullOrEmpty(name))
            {
                errors.Add(CategoryError.NameIsRequired);
            }
            if (string.IsNullOrEmpty(description))
            {
                errors.Add(CategoryError.DescriptionIsRequired);
            }
            return errors;
        }


    }
}
