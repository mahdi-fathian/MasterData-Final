using MasterData.EventDriven.Application.Interfaces;
using MasterData.EventDriven.Domain.Entities;
using MasterData.EventDriven.Domain.Events;
using MasterData.EventDriven.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MasterData.EventDriven.Infrastructure.EventPublishing;

/// <summary>
/// انتشار رویدادها با استفاده از الگوی Outbox - Outbox Pattern Event Publisher
/// </summary>
public class OutboxEventPublisher : IEventPublisher
{
    private readonly EventDrivenDbContext _context;
    private readonly ILogger<OutboxEventPublisher> _logger;

    public OutboxEventPublisher(EventDrivenDbContext context, ILogger<OutboxEventPublisher> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task PublishEventsAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default)
    {
        foreach (var @event in events)
        {
            try
            {
                // ذخیره در Event Store برای Event Sourcing
                var eventStore = new EventStore
                {
                    AggregateId = GetAggregateId(@event),
                    AggregateType = GetAggregateType(@event),
                    EventType = @event.EventType,
                    EventData = JsonSerializer.Serialize(@event),
                    Version = @event.Version,
                    OccurredOn = @event.OccurredOn
                };

                await _context.EventStores.AddAsync(eventStore, cancellationToken);

                // ذخیره در Outbox برای انتشار قابل اطمینان
                var outboxMessage = new OutboxMessage
                {
                    EventType = @event.EventType,
                    EventData = JsonSerializer.Serialize(@event),
                    OccurredOn = @event.OccurredOn
                };

                await _context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

                _logger.LogInformation("رویداد {EventType} در Outbox و EventStore ذخیره شد", @event.EventType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ذخیره رویداد {EventType}", @event.EventType);
                throw;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

    private string GetAggregateId(DomainEvent @event)
    {
        return @event switch
        {
            ProvinceRegisteredEvent e => e.ProvinceId.ToString(),
            ProvinceNameUpdatedEvent e => e.ProvinceId.ToString(),
            ProvinceDeletedEvent e => e.ProvinceId.ToString(),
            _ => "Unknown"
        };
    }

    private string GetAggregateType(DomainEvent @event)
    {
        return @event switch
        {
            ProvinceRegisteredEvent => "Province",
            ProvinceNameUpdatedEvent => "Province",
            ProvinceDeletedEvent => "Province",
            _ => "Unknown"
        };
    }
}
