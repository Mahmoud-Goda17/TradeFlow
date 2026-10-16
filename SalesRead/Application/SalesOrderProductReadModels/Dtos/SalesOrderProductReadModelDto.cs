namespace SalesRead.Application.SalesOrderProductReadModels.Dtos
{
    public class SalesOrderProductReadModelDto
    {
        public Guid SalesOrderId { get;  set; }

        public string OrderNumber { get;  set; } = default!;

        public Guid ProductId { get;  set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal Total { get; set; }

    }
}
