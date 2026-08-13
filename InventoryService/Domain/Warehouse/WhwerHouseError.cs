using Shared.Results;

namespace InventoryService.Domain.Warehouse
{
    public static class WhwerHouseError
    {
        public static Error NameIsErquired => Error.Validation("Name is required", "Warehouse.NameIsErquired");
        public static Error CodeIsErquired => Error.Validation("Code is required", "Warehouse.CodeIsErquired");
        public static Error AddressIsErquired => Error.Validation("Address is required", "Warehouse.AddressIsErquired");
        public static Error WarehouseAlreadyActive => Error.Conflict("Warehouse is already active", "Warehouse.ProductAlreadyActive");
        public static Error WarehouseAlreadyInactive => Error.Conflict("Warehouse is already inactive", "Warehouse.ProductAlreadyInactive");
    }
}
