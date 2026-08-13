using Shared.Commen;
using Shared.Results;

namespace InventoryService.Domain.Stock
{
    public sealed class Stock : AuditableEntity
    {
        public Guid ProductId { get; private set; }

        public Guid WarehouseId { get; private set; }

        public int Quantity { get; private set; }

        public int ReservedQuantity { get; private set; }

        public int AvailableQuantity => Quantity - ReservedQuantity;

        private Stock()
        {
        }

        private Stock(
            Guid id,
            Guid productId,
            Guid warehouseId,
            int quantity)
            : base(id)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            Quantity = quantity;
            ReservedQuantity = 0;
        }
        public static Result<Stock> Create(Guid id,Guid productId,Guid warehouseId, int quantity)
        {
            var errors = Validate(productId, warehouseId, quantity);

            if (errors.Count > 0)
                return errors;

            return new Stock(id, productId,warehouseId, quantity);
        }
        public Result<Updated> Update(Guid productId, Guid warehouseId, int quantity)
        {
            var errors = Validate(productId, warehouseId, quantity);
            if (errors.Count > 0)
                return errors;
            ProductId = productId;
            WarehouseId = warehouseId;
            Quantity = quantity;
            return Result.Updated;
        }
        public Result<Updated> Increase(int quantity)
        {
            if (quantity <= 0)
                return StockError.InvalidQuantity;

            Quantity += quantity;

            return Result.Updated;
        }
        public Result<Updated> Decrease(int quantity)
        {
            if (quantity <= 0)
                return StockError.InvalidQuantity;

            if (AvailableQuantity < quantity)
                return StockError.NotEnoughStock;

            Quantity -= quantity;

            return Result.Updated;
        }
        public Result<Updated> ConfirmReservation(int quantity)
        {
            if (quantity <= 0)
                return StockError.InvalidQuantity;

            if (ReservedQuantity < quantity)
                return StockError.InvalidReservedQuantity;

            Quantity -= quantity;
            ReservedQuantity -= quantity;

            return Result.Updated;
        }
        public Result<Updated> Reserve(int quantity)
        {
            if (quantity <= 0)
                return StockError.InvalidQuantity;

            if (AvailableQuantity < quantity)
                return StockError.NotEnoughStock;

            ReservedQuantity += quantity;

            return Result.Updated;
        }
        public Result<Updated> Release(int quantity)
        {
            if (quantity <= 0)
                return StockError.InvalidQuantity;

            if (ReservedQuantity < quantity)
                return StockError.InvalidReservedQuantity;

            ReservedQuantity -= quantity;

            return Result.Updated;
        }
        private static List<Error> Validate(Guid productId, Guid warehouseId,int quantity)
        {
            List<Error> errors = new List<Error>();

            if (productId == Guid.Empty)
                errors.Add(StockError.InvalidProduct);

            if (warehouseId == Guid.Empty)
                errors.Add(StockError.InvalidWarehouse);

            if (quantity < 0)
                errors.Add(StockError.InvalidQuantity);

            return errors;
        }
    }
}
