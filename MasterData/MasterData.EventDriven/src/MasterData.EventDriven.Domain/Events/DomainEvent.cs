namespace MasterData.EventDriven.Domain.Events;

public abstract class DomainEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    public string EventType { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    public int Version { get; set; } = 1;
}
