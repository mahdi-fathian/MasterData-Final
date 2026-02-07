using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Domain.SeedWork;

namespace MasterData.DDD.Domain.Events;

/// <summary>
/// رویداد دامنه: استان فعال شد - Province Activated Domain Event
/// </summary>
public class ProvinceActivatedDomainEvent : IDomainEvent
{
    public Province Province { get; }
    public DateTime OccurredOn { get; }

    public ProvinceActivatedDomainEvent(Province province)
    {
        Province = province;
        OccurredOn = DateTime.UtcNow;
    }
}
