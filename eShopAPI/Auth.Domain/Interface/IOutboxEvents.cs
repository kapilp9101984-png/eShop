using Auth.Domain.Entity;

namespace Auth.Domain.Interface
{
    public interface IOutboxEvents
    {
        Task<List<OutboxEvents>> GetAllOutboxEvents();
        Task<OutboxEvents> GetOutboxEvents(long ID);
        Task<bool> CreateOutboxEvents(OutboxEvents outboxEvents);
        Task<OutboxEvents> UpdateOutboxEvents(OutboxEvents outboxEvents);
        Task<bool> DeleteOutboxEvents(long ID);
        Task<List<OutboxEvents>> GetUnProcessedMessages(int batchSize);
        Task MarkAsProcessed(List<OutboxEvents> outboxEvents);
    }
}