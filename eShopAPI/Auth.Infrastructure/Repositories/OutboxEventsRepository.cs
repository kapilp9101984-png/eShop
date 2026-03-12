using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Entity;
using Auth.Domain.Interface;
using Auth.Infrastructure.Context;

namespace Auth.Infrastructure.Repositories
{
    public class OutboxEventsRepository : IOutboxEvents
    {
        private readonly AuthDbContext _context;

        public OutboxEventsRepository(AuthDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<OutboxEvents>> GetAllOutboxEvents()
        {
            return await _context.OutboxEvents.ToListAsync();
        }

        public async Task<OutboxEvents> GetOutboxEvents(long ID)
        {
            var entity = await _context.OutboxEvents.FindAsync(ID);
            if (entity is null)
                throw new KeyNotFoundException($"OutboxEvents with ID {ID} not found.");
            return entity;
        }

        public async Task<bool> CreateOutboxEvents(OutboxEvents outboxEvents)
        {
            if (outboxEvents is null) throw new ArgumentNullException(nameof(outboxEvents));
            await _context.OutboxEvents.AddAsync(outboxEvents);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OutboxEvents> UpdateOutboxEvents(OutboxEvents outboxEvents)
        {
            if (outboxEvents is null) throw new ArgumentNullException(nameof(outboxEvents));

            var existing = await _context.OutboxEvents.FindAsync(outboxEvents.ID);
            if (existing is null)
                throw new KeyNotFoundException($"OutboxEvents with ID {outboxEvents.ID} not found.");

            _context.Entry(existing).CurrentValues.SetValues(outboxEvents);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteOutboxEvents(long ID)
        {
            var entity = await _context.OutboxEvents.FindAsync(ID);
            if (entity is null) return false;

            _context.OutboxEvents.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OutboxEvents>> GetUnProcessedMessages(int batchSize)
        {
            return await _context.OutboxEvents
          .Where(x => !x.IsCompleted && x.ProcessedAt == null)
          .OrderBy(x => x.CreatedAt)
          .Take(batchSize)
          .ToListAsync();
        }

        public async Task MarkAsProcessed(List<OutboxEvents> outboxEvents)
        {
            foreach (OutboxEvents item in outboxEvents)
            {
                var existing = await _context.OutboxEvents.FindAsync(item.ID);
                if (existing is not null)
                {
                    existing.IsCompleted = true;
                    existing.ProcessedAt = DateTime.UtcNow;
                    _context.OutboxEvents.Update(existing);
                }
                _context.SaveChanges();
            }

        }
    }
}
