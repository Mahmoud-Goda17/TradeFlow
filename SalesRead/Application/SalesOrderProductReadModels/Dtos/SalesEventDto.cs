namespace SalesRead.Application.SalesOrderProductReadModels.Dtos
{
    public sealed class SalesEventDto
    {
        public Guid EventId { get; set; }
        public Guid SalesOrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}
