using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SalesRead.Application.Processors;
using SalesRead.Application.SalesOrderProductReadModels.Dtos;
using Shared.Commen;

namespace SalesRead.InfraStrucure.Consumers
{
    public sealed class RabbitMqConsumerService(IOptions<RappitMqConfiguration> options
        , ILogger<RabbitMqConsumerService> logger,ProductEventProcessor productEventProcessor
        , SalesEventProcessor salesEventProcessor) : BackgroundService
    {
        private readonly RappitMqConfiguration _configuration = options.Value;
        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.HostName,
                UserName = _configuration.UserName,
                Password = _configuration.Password
            };

            using var connection = await factory.CreateConnectionAsync(stoppingToken);

            using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(
                queue: _configuration.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null,
                cancellationToken: stoppingToken);

            logger.LogInformation("Connected to RabbitMQ. Queue: {QueueName}",_configuration.QueueName);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                using var jsonDocument = JsonDocument.Parse(message);
                var root = jsonDocument.RootElement;
                var eventType = root.GetProperty("EventType").GetString();
                var data = root.GetProperty("Data");
                switch (eventType)
                {
                    case "SalesEvent":
                        {
                            var salesEvent = data.Deserialize<SalesEventDto>();
                            if (salesEvent is null)
                            {
                                logger.LogWarning("SalesEvent data is empty.");
                                return;
                            }
                            await salesEventProcessor.ProcessAsync(salesEvent,stoppingToken);
                            logger.LogInformation(
                                "SalesEvent processed successfully for Order: {OrderNumber}, Product: {ProductId}",
                                salesEvent.OrderNumber,
                                salesEvent.ProductId);
                            break;
                        }
                    case "ProductEvent":
                        {
                            var productEvent = data.Deserialize<ProductEventDto>();
                            if (productEvent is null)
                            {
                                logger.LogWarning("ProductEvent data is empty.");
                                return;
                            }
                            await productEventProcessor.ProcessAsync(productEvent,stoppingToken);
                            logger.LogInformation(
                                "ProductEvent processed successfully for Product: {ProductId}, Name: {ProductName}",
                                productEvent.ProductId,
                                productEvent.ProductName);
                            break;
                        }
                    default:
                        logger.LogWarning("Unknown EventType received: {EventType}",eventType);
                        break;
                }
                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: _configuration.QueueName,
                autoAck: true,
                consumer: consumer,
                cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
