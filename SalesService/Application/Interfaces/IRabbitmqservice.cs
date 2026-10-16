namespace SalesService.Application.Interfaces
{
    public interface IRabbitmqservice
    {
        Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken = default);
    }
}
