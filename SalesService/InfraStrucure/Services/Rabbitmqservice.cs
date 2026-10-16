using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using SalesService.Application.Interfaces;
using Shared.Commen;
using System.Text.Json;
using System.Text;

namespace SalesService.InfraStrucure.Services
{
    public sealed class Rabbitmqservice : IRabbitmqservice
    {
        private readonly RappitMqConfiguration _configuration;
        private readonly ConnectionFactory _connectionFactory;
        public Rabbitmqservice(IOptions<RappitMqConfiguration> options)
        {
            _configuration = options.Value;
            _connectionFactory = new ConnectionFactory
            {
                HostName = _configuration.HostName,
                UserName = _configuration.UserName,
                Password = _configuration.Password
            };
        }
        public async Task PublishMessageAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            await using var connection = await _connectionFactory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: _configuration.QueueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);
            var envelope = new
            {
                EventType = typeof(T).Name,
                Data = message
            };

            var json = JsonSerializer.Serialize(envelope);

            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(exchange: "",
                                            routingKey: _configuration.QueueName,
                                            body: body,
                                            cancellationToken: cancellationToken);
        }
    }
}
