using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Auth.Infrastructure.BackgroundServices
{
    public class NotificationServices(IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        private IServiceScopeFactory ServiceScopeFactory { get; } = serviceScopeFactory;
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = ServiceScopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IOutboxEvents>();

                List<OutboxEvents> unprocessedMessages = await repository.GetUnProcessedMessages(10);

                foreach (OutboxEvents eventItem in unprocessedMessages)
                {
                    /// Here you can implement the logic to send the notification based on the event type and payload to rabbitmq & Kafka (e.g., using an email service, SMS service etc.)
                    await RabbitMQPublisher.Publish(eventItem.EventType, eventItem.Payload);
                    eventItem.RetryCount = eventItem.RetryCount + 1;
                }

                 await repository.MarkAsProcessed(unprocessedMessages);

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);

            }

        }
    }
}
