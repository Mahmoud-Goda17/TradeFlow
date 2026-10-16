using MediatR;
using SalesService.Application.Interfaces;
using SalesService.Domain.Event;

namespace SalesService.Application.Featuers.SalesOrdes.EventHandler
{
    public sealed class SalesEventHandler(IRabbitmqservice rabbitmqservice):INotificationHandler<SalesEvent>
    {
        public async Task Handle(SalesEvent notification, CancellationToken cancellationToken)
        {
            await rabbitmqservice.PublishMessageAsync(notification,cancellationToken);
        }
    }
}
