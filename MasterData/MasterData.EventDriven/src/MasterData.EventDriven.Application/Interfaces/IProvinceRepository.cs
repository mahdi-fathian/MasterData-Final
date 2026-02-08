using MasterData.EventDriven.Domain.Entities;

namespace MasterData.EventDriven.Application.Interfaces;

public interface IProvinceRepository
{
    Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId, CancellationToken cancellationToken = default);
    Task AddAsync(Province province, CancellationToken cancellationToken = default);
    void Update(Province province);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
