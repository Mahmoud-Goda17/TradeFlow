using SalesService.Domain.SalesOrders;

namespace SalesService.Application.Featuers.SalesOrdes.Dtos
{
    public class SalesOrderDto
    {
        public string OrderNumber { get;  set; } = default!;

        public Guid CustomerId { get;  set; }

        public OrderStatus Status { get;  set; }

        public decimal Subtotal { get;  set; }
        public List<SalesOrderItemDtos> items { get; set; } = [];


    }
}
