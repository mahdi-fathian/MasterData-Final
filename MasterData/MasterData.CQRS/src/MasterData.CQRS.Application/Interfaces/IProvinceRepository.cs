using MasterData.CQRS.Domain.Entities;

namespace MasterData.CQRS.Application.Interfaces;

/// <summary>
/// رابط مخزن استان برای عملیات نوشتاری - Province Repository Interface for Write Operations
/// </summary>
public interface IProvinceRepository
{
    Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId, CancellationToken cancellationToken = default);
    Task AddAsync(Province province, CancellationToken cancellationToken = default);
    void Update(Province province);
    void Delete(Province province);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
