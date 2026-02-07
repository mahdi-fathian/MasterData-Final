using MasterData.CQRS.Domain.Entities;

namespace MasterData.CQRS.Application.Interfaces;

/// <summary>
/// رابط مخزن استان برای عملیات خواندنی - Province Read Repository Interface for Read Operations
/// </summary>
public interface IProvinceReadRepository
{
    Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default);
}
