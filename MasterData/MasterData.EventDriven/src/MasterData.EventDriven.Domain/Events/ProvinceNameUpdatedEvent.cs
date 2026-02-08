namespace MasterData.EventDriven.Domain.Events;


public class ProvinceNameUpdatedEvent : DomainEvent
{
    public int ProvinceId { get; set; }
    public string OldName { get; set; } = string.Empty;
    public string NewName { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }

    public ProvinceNameUpdatedEvent()
    {
        EventType = nameof(ProvinceNameUpdatedEvent);
    }

    public ProvinceNameUpdatedEvent(int provinceId, string oldName, string newName, DateTime updatedAt)
    {
        ProvinceId = provinceId;
        OldName = oldName;
        NewName = newName;
        UpdatedAt = updatedAt;
        EventType = nameof(ProvinceNameUpdatedEvent);
        OccurredOn = DateTime.UtcNow;
    }
}
