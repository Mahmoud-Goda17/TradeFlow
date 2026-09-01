using SalesService.Domain.SalesOrderItems;
using Shared.Commen;
using Shared.Results;

namespace SalesService.Domain.SalesOrders
{
    public sealed class SalesOrder:AuditableEntity
    {
        public string OrderNumber { get; private set; } = default!;

        public Guid CustomerId { get; private set; }

        public OrderStatus Status { get; private set; }

        public decimal Subtotal { get; private set; }

        public decimal Discount { get; private set; }

        public decimal Tax { get; private set; }

        public decimal Total { get; private set; }

        public string ShippingAddress { get; private set; } = default!;
        private readonly List<SalesOrderItem> _items = [];//لازم دي سمال
        public IEnumerable<SalesOrderItem> Items => _items.AsReadOnly();//لازم دي كابيتال
        private SalesOrder()
        {
        }
        private SalesOrder(Guid id, string orderNumber, Guid customerId,string shippingAddress):base(id)
        {
            OrderNumber = orderNumber;
            CustomerId = customerId;
            ShippingAddress = shippingAddress;

            Status = OrderStatus.Pending;
        }
        public static Result<SalesOrder> Create(string orderNumber,Guid customerId,  string shippingAddress)
        {
            var errors = Validate( orderNumber, customerId, shippingAddress);

            if (errors.Count > 0)
                return errors;

            var order = new SalesOrder(Guid.NewGuid(), orderNumber,customerId,shippingAddress);

            return order;
        }
        public Result<Updated> AddItem(Guid productId,  int quantity, decimal unitPrice, decimal discount, decimal tax)
        {
            if (Status != OrderStatus.Pending)
                return SalesOrderError.OrderNotPending;

            var existingItem = _items.FirstOrDefault(
                x => x.ProductId == productId);

            if (existingItem is not null)
            {
                var result = existingItem.UpdateQuantity(
                    existingItem.Quantity + quantity);

                if (result.IsError)
                    return result.Errors;

                RecalculateTotals();

                return Result.Updated;
            }

            var itemResult = SalesOrderItem.Create(
                Id,
                productId,
                quantity,
                unitPrice,
                discount,
                tax);

            if (itemResult.IsError)
                return itemResult.Errors;

            _items.Add(itemResult.Value);

            RecalculateTotals();

            return Result.Updated;
        }
        public Result<Updated> RemoveItem(Guid productId)
        {
            if (Status != OrderStatus.Pending)
                return SalesOrderError.OrderNotPending;

            var item = _items.FirstOrDefault(
                x => x.ProductId == productId);

            if (item is null)
                return SalesOrderError.ItemNotFound;

            _items.Remove(item);

            RecalculateTotals();

            return Result.Updated;
        }
        public Result<Updated> UpdateItemQuantity( Guid productId, int quantity)
        {
            if (Status != OrderStatus.Pending)
                return SalesOrderError.OrderNotPending;

            var item = _items.FirstOrDefault(
                x => x.ProductId == productId);

            if (item is null)
                return SalesOrderError.ItemNotFound;

            var result = item.UpdateQuantity(quantity);

            if (result.IsError)
                return result.Errors;

            RecalculateTotals();

            return Result.Updated;
        }
        public Result<Updated> UpdateItemDiscount( Guid productId, decimal discount)
        {
            if (Status != OrderStatus.Pending)
                return SalesOrderError.OrderNotPending;

            var item = _items.FirstOrDefault(
                x => x.ProductId == productId);

            if (item is null)
                return SalesOrderError.ItemNotFound;

            var result = item.UpdateDiscount(discount);

            if (result.IsError)
                return result.Errors;

            RecalculateTotals();

            return Result.Updated;
        }
        public Result<Updated> UpdateItemTax(Guid productId,decimal Tax)
        {
            if (Status != OrderStatus.Pending)
                return SalesOrderError.OrderNotPending;

            var item = _items.FirstOrDefault(
                x => x.ProductId == productId);

            if (item is null)
                return SalesOrderError.ItemNotFound;

            var result = item.UpdateTax(Tax);

            if (result.IsError)
                return result.Errors;

            RecalculateTotals();

            return Result.Updated;
        }
        public Result<Updated> Confirm()
        {
            if (Status != OrderStatus.Pending)
                return SalesOrderError.InvalidStatusTransition;

            if (_items.Count == 0)
                return SalesOrderError.OrderHasNoItems;

            Status = OrderStatus.Confirmed;

            return Result.Updated;
        }
        public Result<Updated> Complete()
        {
            if (Status != OrderStatus.Confirmed)
                return SalesOrderError.InvalidStatusTransition;

            Status = OrderStatus.Completed;

            return Result.Updated;
        }
        public Result<Updated> Cancel()
        {
            if (Status == OrderStatus.Completed)
                return SalesOrderError.CannotCancelCompletedOrder;

            if (Status == OrderStatus.Cancelled)
                return SalesOrderError.OrderAlreadyCancelled;

            Status = OrderStatus.Cancelled;

            return Result.Updated;
        }
        private void RecalculateTotals()
        {
            Subtotal = _items.Sum(
                x => x.Quantity * x.UnitPrice);

            Discount = _items.Sum(
                x => x.Discount);

            Tax = _items.Sum(
                x => x.Tax);

            Total = Subtotal - Discount + Tax;
        }
        private static List<Error> Validate( string orderNumber, Guid customerId, string shippingAddress)
        {
            var errors = new List<Error>();

            if (string.IsNullOrWhiteSpace(orderNumber))
                errors.Add(SalesOrderError.InvalidOrderNumber);

            if (customerId == Guid.Empty)
                errors.Add(SalesOrderError.InvalidCustomerId);

            if (string.IsNullOrWhiteSpace(shippingAddress))
                errors.Add(SalesOrderError.InvalidShippingAddress);

            return errors;
        }

    }
}
