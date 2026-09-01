namespace SalesService.Application.Featuers.SalesOrdes.Dtos
{
    public class SalesOrderItemDtos
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
