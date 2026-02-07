namespace MasterData.DDD.Domain.SeedWork;

/// <summary>
/// رابط رویداد دامنه - Domain Event Interface
/// </summary>
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
