namespace SalesRead.Application.SalesOrderProductReadModels.Dtos
{
    public sealed class ProductEventDto
    {
        public Guid EventId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }
}
