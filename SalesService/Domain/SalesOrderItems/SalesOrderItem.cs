using Shared.Commen;
using Shared.Results;

namespace SalesService.Domain.SalesOrderItems
{
    public sealed class SalesOrderItem: AuditableEntity
    {
        public Guid SalesOrderId { get; private set; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Discount { get; private set; }

        public decimal Tax { get; private set; }

        public decimal Total { get; private set; }
        private SalesOrderItem()
        {
        }

        private SalesOrderItem(
            Guid id,
            Guid salesOrderId,
            Guid productId,
            int quantity,
            decimal unitPrice,
            decimal discount,
            decimal tax)
            : base(id)
        {
            SalesOrderId = salesOrderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            Tax = tax;

            CalculateTotal();
        }


        internal static Result<SalesOrderItem> Create(
            Guid salesOrderId,
            Guid productId,
            int quantity,
            decimal unitPrice,
            decimal discount,
            decimal tax)
        {
            var errors = Validate(
                salesOrderId,
                productId,
                quantity,
                unitPrice,
                discount,
                tax);

            if (errors.Count > 0)
                return errors;

            var item = new SalesOrderItem(
                Guid.NewGuid(),
                salesOrderId,
                productId,
                quantity,
                unitPrice,
                discount,
                tax);

            return item;
        }


        internal Result<Updated> UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                return SalesOrderItemError.InvalidQuantity;

            Quantity = quantity;

            CalculateTotal();

            return Result.Updated;
        }


        internal Result<Updated> UpdateDiscount(decimal discount)
        {
            if (discount < 0)
                return SalesOrderItemError.InvalidDiscount;

            var grossTotal = Quantity * UnitPrice;

            if (discount > grossTotal)
                return SalesOrderItemError.InvalidDiscount;

            Discount = discount;

            CalculateTotal();

            return Result.Updated;
        }


        internal Result<Updated> UpdateTax(decimal tax)
        {
            if (tax < 0)
                return SalesOrderItemError.InvalidTax;

            Tax = tax;

            CalculateTotal();

            return Result.Updated;
        }


        private void CalculateTotal()
        {
            Total = (Quantity * UnitPrice) - Discount + Tax;
        }


        private static List<Error> Validate(
            Guid salesOrderId,
            Guid productId,
            int quantity,
            decimal unitPrice,
            decimal discount,
            decimal tax)
        {
            var errors = new List<Error>();

            if (salesOrderId == Guid.Empty)
                errors.Add(SalesOrderItemError.InvalidOrderId);

            if (productId == Guid.Empty)
                errors.Add(SalesOrderItemError.InvalidProductId);

            if (quantity <= 0)
                errors.Add(SalesOrderItemError.InvalidQuantity);

            if (unitPrice < 0)
                errors.Add(SalesOrderItemError.InvalidUnitPrice);

            if (discount < 0)
                errors.Add(SalesOrderItemError.InvalidDiscount);

            if (tax < 0)
                errors.Add(SalesOrderItemError.InvalidTax);

            if (discount > quantity * unitPrice)
                errors.Add(SalesOrderItemError.InvalidDiscount);

            return errors;
        }
    }
}
