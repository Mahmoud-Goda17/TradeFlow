using Shared.Results;

namespace ProductSercice.Domain.Category
{
    public static class CategoryError
    {
        public static Error NameIsRequired =>
                  Error.Validation("Category.NameIsRequired", "Category name is required.");
        public static Error DescriptionIsRequired =>
            Error.Validation("Category.DescriptionIsRequired", "Category description is required.");

    }
}
