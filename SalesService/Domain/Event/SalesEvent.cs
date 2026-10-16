using Shared.Commen;

namespace SalesService.Domain.Event;

public sealed class SalesEvent : DomainEvent
{
    public Guid EventId { get; }
    public Guid SalesOrderId { get; }
    public string OrderNumber { get; }
    public Guid ProductId { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal Discount { get; }
    public decimal Tax { get; }
    public decimal Total { get; }

    public SalesEvent(
        Guid eventId,
        Guid salesOrderId,
        string orderNumber,
        Guid productId,
        int quantity,
        decimal unitPrice,
        decimal discount,
        decimal tax,
        decimal total)
    {
        EventId = eventId;
        SalesOrderId = salesOrderId;
        OrderNumber = orderNumber;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
        Tax = tax;
        Total = total;
    }
}