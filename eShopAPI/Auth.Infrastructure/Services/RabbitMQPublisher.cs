/* Pseudocode / Plan (detailed):
   - Open connection factory with hostname and credentials.
   - Create async connection and channel.
   - Declare exchange "AuthExchange" (direct, durable).
   - Declare queue "AuthQueue" (durable, non-exclusive, no auto-delete).
   - Bind queue "AuthQueue" to exchange "AuthExchange" with routing key "AuthService".
   - Encode Message to UTF8 bytes as Memory<byte>.
   - Create BasicProperties:
     - set Type to EventType
     - set DeliveryMode to Persistent
     - set Headers to an IDictionary<string, object?> where values are nullable (object?)
       - use key "eventType" and value EventType (string is non-null here)
   - Publish message to exchange with routing key "AuthService" using BasicPublishAsync.
   - Dispose channel and connection asynchronously with await using.
   - The fix: ensure the Headers dictionary type matches expected 'IDictionary<string, object?>'
     by creating a Dictionary<string, object?> (note the nullable value type).
*/

using RabbitMQ.Client;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Services
{
    public static class RabbitMQPublisher
    {
        public static async Task Publish(string EventType, string Message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };

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

            // obtain IBasicProperties from the underlying IModel exposed by IChannel

            BasicProperties properties = new BasicProperties
            {
                Type = EventType,
                DeliveryMode = DeliveryModes.Persistent,
                Headers = new Dictionary<string, object?> { ["eventType"] = EventType } // fixed nullability: object?
            };

            await channel.BasicPublishAsync(
                exchange: "AuthExchange",
                routingKey: "AuthService",
                mandatory: false,
                basicProperties: properties,
                body: body);
        }
    }
}