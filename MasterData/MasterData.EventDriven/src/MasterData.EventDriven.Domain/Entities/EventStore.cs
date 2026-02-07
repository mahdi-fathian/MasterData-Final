namespace MasterData.EventDriven.Domain.Entities;

/// <summary>
/// ذخیره‌ساز رویدادها - Event Store for Event Sourcing
/// </summary>
public class EventStore
{
    public Guid Id { get; set; }
    public string AggregateId { get; set; } = string.Empty;
    public string AggregateType { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;
    public int Version { get; set; }
    public DateTime OccurredOn { get; set; }

    public EventStore()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }
}
