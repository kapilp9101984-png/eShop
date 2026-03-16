using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification.Infrasturcture.Service
{
    public class NotificationRabbitMQService : BackgroundService
    {
        private IConnection Connection { get; set; }
        private IChannel Channel { get; set; }
        public NotificationRabbitMQService()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            Connection = factory.CreateConnectionAsync().Result;
            Channel = Connection.CreateChannelAsync().Result;
            Channel.ExchangeDeclareAsync(exchange: "AuthExchange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            Channel.QueueDeclareAsync(queue: "AuthQueue",
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
            Channel.QueueBindAsync(queue: "AuthQueue", exchange: "AuthExchange", routingKey: "AuthService");
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(Channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var eventType = ea.BasicProperties.Type;
                var message = System.Text.Encoding.UTF8.GetString(body);

                if (eventType == "UserCreated")
                {
                    // if user created then send verification email to user email address using message payload (e.g., deserialize message to get email and token)
                }
                // Here you can implement the logic to process the received message and send notification (e.g., using an email service, SMS service etc.)
                await Channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            await Channel.BasicConsumeAsync(queue: "AuthQueue", autoAck: false, consumer: consumer);

        }

        public override void Dispose()
        {
            Channel?.CloseAsync();
            Connection?.CloseAsync();
            base.Dispose();
        }

    }
}
