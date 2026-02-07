using MasterData.EventDriven.Domain.Events;

namespace MasterData.EventDriven.Application.Interfaces;

/// <summary>
/// رابط انتشار رویدادها - Event Publisher Interface
/// </summary>
public interface IEventPublisher
{
    Task PublishEventsAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default);
}
