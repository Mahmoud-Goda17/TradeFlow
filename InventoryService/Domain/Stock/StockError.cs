using Shared.Results;

namespace InventoryService.Domain.Stock
{
    public static class StockError
    {
        public static Error InvalidProduct => Error.Validation("Invalid product", "Stock.InvalidProduct");
        public static Error InvalidWarehouse => Error.Validation("Invalid warehouse", "Stock.InvalidWarehouse");
        public static Error InvalidQuantity => Error.Validation("Invalid quantity", "Stock.InvalidQuantity");
        public static Error NotEnoughStock => Error.Conflict("Not enough stock", "Stock.NotEnoughStock");
        public static Error InvalidReservedQuantity => Error.Validation("Invalid reserved quantity", "Stock.InvalidReservedQuantity");
    }
}
