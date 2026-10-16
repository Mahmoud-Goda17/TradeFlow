using MediatR;
using ProductSercice.Application.Interfaces;
using ProductSercice.Domain.Event;

namespace ProductSercice.Application.Featuers.Products.EventHandler
{
    public sealed class ProductEventHandler(IRabbitmqservice rabbitmqservice): INotificationHandler<ProductEvent>
    {
        public async Task Handle( ProductEvent notification,CancellationToken cancellationToken)
        {
            await rabbitmqservice.PublishMessageAsync(notification,cancellationToken);
        }
    }
}
