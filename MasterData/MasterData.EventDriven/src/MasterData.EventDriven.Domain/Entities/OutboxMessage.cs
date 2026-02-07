namespace MasterData.EventDriven.Domain.Entities;

/// <summary>
/// پیام صندوق خروجی - Outbox Message for reliable event publishing
/// </summary>
public class OutboxMessage
{
    public Guid Id { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string EventData { get; set; } = string.Empty;
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public bool IsProcessed { get; set; }
    public int RetryCount { get; set; }
    public string? Error { get; set; }

    public OutboxMessage()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        IsProcessed = false;
        RetryCount = 0;
    }
}
