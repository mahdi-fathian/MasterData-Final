using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Domain.SeedWork;

namespace MasterData.DDD.Domain.Events;


public class ProvinceNameChangedDomainEvent : IDomainEvent
{
    public Province Province { get; }
    public string OldName { get; }
    public string NewName { get; }
    public DateTime OccurredOn { get; }

    public ProvinceNameChangedDomainEvent(Province province, string oldName, string newName)
    {
        Province = province;
        OldName = oldName;
        NewName = newName;
        OccurredOn = DateTime.UtcNow;
    }
}
