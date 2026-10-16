using Shared.Commen;

namespace ProductSercice.Domain.Event
{
    public sealed class ProductEvent : DomainEvent
    {
        public Guid EventId { get; }
        public Guid ProductId { get; }
        public string ProductName { get; }

        public ProductEvent(Guid eventId,Guid productId,string productName)
        {
            EventId = eventId;
            ProductId = productId;
            ProductName = productName;
        }
    }
}
