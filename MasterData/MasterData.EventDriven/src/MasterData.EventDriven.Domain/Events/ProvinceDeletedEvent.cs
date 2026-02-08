namespace MasterData.EventDriven.Domain.Events;

public class ProvinceDeletedEvent : DomainEvent
{
    public int ProvinceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DeletedAt { get; set; }

    public ProvinceDeletedEvent()
    {
        EventType = nameof(ProvinceDeletedEvent);
    }

    public ProvinceDeletedEvent(int provinceId, string name, DateTime deletedAt)
    {
        ProvinceId = provinceId;
        Name = name;
        DeletedAt = deletedAt;
        EventType = nameof(ProvinceDeletedEvent);
        OccurredOn = DateTime.UtcNow;
    }
}
