using Shared.Results;

namespace UserService.Application
{
    public static class AppErrors
    {
        public static Error NotFound(string entityName, Guid id) =>
            Error.NotFound($"{entityName}.NotFound", $"{entityName} with ID '{id}' was not found.");
        public static Error IsExcist(string entityName, string fieldName, string fieldValue) =>
            Error.Conflict($"{entityName}.IsExist", $"{entityName} with {fieldName} '{fieldValue}' already exists.");

    }
}
