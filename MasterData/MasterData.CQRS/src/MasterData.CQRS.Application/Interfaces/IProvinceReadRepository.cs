using MasterData.CQRS.Domain.Entities;

namespace MasterData.CQRS.Application.Interfaces;


public interface IProvinceReadRepository
{
    Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default);
}
