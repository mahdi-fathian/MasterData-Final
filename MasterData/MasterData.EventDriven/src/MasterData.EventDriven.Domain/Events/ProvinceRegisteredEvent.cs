namespace MasterData.EventDriven.Domain.Events;

/// <summary>
/// رویداد: استان ثبت شد - Province Registered Event
/// </summary>
public class ProvinceRegisteredEvent : DomainEvent
{
    public int ProvinceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime RegisteredAt { get; set; }

    public ProvinceRegisteredEvent()
    {
        EventType = nameof(ProvinceRegisteredEvent);
    }

    public ProvinceRegisteredEvent(int provinceId, string name, DateTime registeredAt)
    {
        ProvinceId = provinceId;
        Name = name;
        RegisteredAt = registeredAt;
        EventType = nameof(ProvinceRegisteredEvent);
        OccurredOn = DateTime.UtcNow;
    }
}
