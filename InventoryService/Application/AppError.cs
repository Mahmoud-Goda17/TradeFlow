using Shared.Results;

namespace InventoryService.Application
{
    public static class AppError
    {
        public static Error NotFound(string entityName, string id) =>
            Error.NotFound($"{entityName}.NotFound", $"{entityName} with ID '{id}' was not found.");
        public static Error IsExcist(string entityName, string fieldName, string fieldValue) =>
            Error.Conflict($"{entityName}.IsExist", $"{entityName} with {fieldName} '{fieldValue}' already exists.");
        public static Error IsExcist(string entityName, string fieldName1, string fieldName2, string fieldValue1, string fieldValue2) =>
           Error.Conflict($"{entityName}.IsExist", $"{entityName} with {fieldName1} '{fieldValue1}' OR {fieldName2} '{fieldValue2}' already exists.");
        public static Error NotFoundByProductId(string entityName, string productId) =>
            Error.NotFound($"{entityName}.NotFoundByProductId", $"{entityName} with Product ID '{productId}' was not found.");

    }
}
