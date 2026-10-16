using Shared.Commen;

namespace SalesRead.Damain.SalesOrderProductReadModels
{
    public sealed class SalesOrderProductReadModel : AuditableEntity
    {
        public Guid SalesOrderId { get; private set; }

        public string OrderNumber { get; private set; } = default!;

        public Guid ProductId { get; private set; }

        public string ProductName { get; private set; } = default!;

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Discount { get; private set; }

        public decimal Tax { get; private set; }

        public decimal Total { get; private set; }
        private SalesOrderProductReadModel()
        {
        }

        public SalesOrderProductReadModel(Guid salesOrderId,string orderNumber,Guid productId,string productName,int quantity,
            decimal unitPrice,decimal discount,decimal tax,decimal total)
        {
            SalesOrderId = salesOrderId;
            OrderNumber = orderNumber;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            Tax = tax;
            Total = total;
        }

        public void Update(string orderNumber,string productName,int quantity,decimal unitPrice,decimal discount,decimal tax,
           decimal total)
        {
            OrderNumber = orderNumber;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            Tax = tax;
            Total = total;
        }
    }
}

