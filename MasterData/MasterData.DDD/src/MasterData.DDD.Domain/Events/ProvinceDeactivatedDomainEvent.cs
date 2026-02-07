using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Domain.SeedWork;

namespace MasterData.DDD.Domain.Events;

/// <summary>
/// رویداد دامنه: استان غیرفعال شد - Province Deactivated Domain Event
/// </summary>
public class ProvinceDeactivatedDomainEvent : IDomainEvent
{
    public Province Province { get; }
    public DateTime OccurredOn { get; }

    public ProvinceDeactivatedDomainEvent(Province province)
    {
        Province = province;
        OccurredOn = DateTime.UtcNow;
    }
}
