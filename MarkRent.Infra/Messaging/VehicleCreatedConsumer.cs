using MarkRent.Domain.Entities;
using MarkRent.Domain;
using MarkRent.Domain.Interfaces.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MarkRent.Infra.Messaging
{
    public class VehicleCreatedConsumer : BackgroundService
    {
        private readonly RabbitMQSettings _rabbitMQSettings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public VehicleCreatedConsumer(IOptions<RabbitMQSettings> rabbitMqOptions, IServiceScopeFactory serviceScopeFactory)
        {
            _rabbitMQSettings = rabbitMqOptions.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabbitMQSettings.Host,
                UserName = _rabbitMQSettings.Username,
                Password = _rabbitMQSettings.Password
            };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: "VehicleCreate", ExchangeType.Direct);
            await channel.QueueDeclareAsync(queue: _rabbitMQSettings.QueueName, durable: true, exclusive: false);

            await channel.QueueBindAsync(_rabbitMQSettings.QueueName, "VehicleCreate", "");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (sender, args) =>
            {
                var body = args.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var vehicle = JsonSerializer.Deserialize<Vehicle>(message);

                // Processamento do veículo
                if (vehicle?.Year == 2024)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var vehicleRepository = scope.ServiceProvider.GetRequiredService<IVehicleRepository>();

                        var futureEvent = new FutureEvent
                        {
                            Id = Guid.NewGuid(),
                            VehicleId = vehicle.Id,
                            Model = vehicle.Model
                        };

                        await vehicleRepository.CreateFutureEvent(futureEvent);
                    }
                }

                await channel.BasicAckAsync(deliveryTag: args.DeliveryTag, multiple: false);
            };

            await channel.BasicConsumeAsync(queue: _rabbitMQSettings.QueueName, autoAck: false, consumer: consumer);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
    }
}
