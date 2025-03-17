using MarkRent.Domain;
using MarkRent.Domain.Entities;
using MarkRent.Domain.Interfaces.Messaging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace MarkRent.Infra.Messaging
{
    public class VehicleCreatedPublisher : IVehiclePublisher
    {
        private readonly RabbitMQSettings _rabbitMQSettings;

        public VehicleCreatedPublisher(IOptions<RabbitMQSettings> rabbitMqOptions)
        {
            _rabbitMQSettings = rabbitMqOptions.Value;
        }

        public async Task Publish(Vehicle vehicle)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQSettings.Host,
                UserName = _rabbitMQSettings.Username,
                Password = _rabbitMQSettings.Password
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync(); // Usar o CreateChannelAsync aqui

            var message = JsonSerializer.Serialize(vehicle);
            var body = Encoding.UTF8.GetBytes(message);

            // Publicar a mensagem na fila
            await channel.BasicPublishAsync(
                exchange: "VehicleCreate",
                routingKey: "",
                body: body
            );
        }
    }
}
