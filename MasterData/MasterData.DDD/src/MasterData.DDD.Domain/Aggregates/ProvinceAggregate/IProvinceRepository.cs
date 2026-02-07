using MasterData.DDD.Domain.SeedWork;

namespace MasterData.DDD.Domain.Aggregates.ProvinceAggregate;

/// <summary>
/// رابط مخزن استان - Province Repository Interface
/// </summary>
public interface IProvinceRepository
{
    Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Province?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Province>> GetActiveProvincesAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId, CancellationToken cancellationToken = default);
    Task AddAsync(Province province, CancellationToken cancellationToken = default);
    void Update(Province province);
    void Remove(Province province);
}
