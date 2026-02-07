using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Domain.SeedWork;

namespace MasterData.DDD.Domain.Events;

/// <summary>
/// رویداد دامنه: استان ایجاد شد - Province Created Domain Event
/// </summary>
public class ProvinceCreatedDomainEvent : IDomainEvent
{
    public Province Province { get; }
    public DateTime OccurredOn { get; }

    public ProvinceCreatedDomainEvent(Province province)
    {
        Province = province;
        OccurredOn = DateTime.UtcNow;
    }
}
