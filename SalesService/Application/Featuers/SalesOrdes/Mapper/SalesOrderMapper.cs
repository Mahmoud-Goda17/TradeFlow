using System.Linq.Expressions;
using SalesService.Application.Featuers.SalesOrdes.Dtos;
using SalesService.Domain.SalesOrders;

namespace SalesService.Application.Featuers.SalesOrdes.Mapper
{
    public static class SalesOrderMapper
    {
        public static SalesOrderDto ToSalesOrderDto(this SalesOrder salesOrder)
        {
            return new SalesOrderDto
            {
                OrderNumber = salesOrder.OrderNumber,
                CustomerId = salesOrder.CustomerId,
                Status = salesOrder.Status,
                Subtotal = salesOrder.Subtotal,
                items = salesOrder.Items.Select(item => new SalesOrderItemDtos
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }
        public static readonly Expression<Func<SalesOrder,SalesOrderDto>> ToDto =
            s => new SalesOrderDto
            {
                OrderNumber = s.OrderNumber,
                CustomerId = s.CustomerId,
                Status = s.Status,
                Subtotal = s.Subtotal,
                items = s.Items.Select(item => new SalesOrderItemDtos
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
    }
}
