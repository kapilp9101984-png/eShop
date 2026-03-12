using RabbitMQ.Client;
using System.Text;

namespace Auth.Infrastructure.Services
{
    public static class RabbitMQPublisher
    {
        // Use a Task-returning method (not async void) and use the synchronous RabbitMQ API to ensure compatibility
        public static async Task Publish(string EventType, string Message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            // create connection and channel using async API available in RabbitMQ.Client 7+
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            // ensure exchange and queue exist
            await channel.ExchangeDeclareAsync(exchange: "AuthExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            await channel.QueueDeclareAsync(queue: "AuthQueue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
            await channel.QueueBindAsync(queue: "AuthQueue", exchange: "AuthExchange", routingKey: "AuthService");

            var body = Encoding.UTF8.GetBytes(Message).AsMemory();

            // publish using the async API; use ReadOnlyMemory<byte> overload
            await channel.BasicPublishAsync(exchange: "AuthExchange",
                                 routingKey: "AuthService",
                                 mandatory: false,
                                 body: body);
        }
    }
}
