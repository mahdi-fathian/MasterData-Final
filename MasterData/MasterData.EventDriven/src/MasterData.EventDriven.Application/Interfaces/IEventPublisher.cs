using MasterData.EventDriven.Domain.Events;

namespace MasterData.EventDriven.Application.Interfaces;

public interface IEventPublisher
{
    Task PublishEventsAsync(IEnumerable<DomainEvent> events, CancellationToken cancellationToken = default);
}
