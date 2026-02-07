using MasterData.DDD.Application.DTOs;

namespace MasterData.DDD.Application.Services;

/// <summary>
/// رابط سرویس استان - Province Service Interface
/// </summary>
public interface IProvinceService
{
    Task<ProvinceDto> CreateProvinceAsync(CreateProvinceRequest request, CancellationToken cancellationToken = default);
    Task<ProvinceDto?> GetProvinceByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProvinceDto>> GetAllProvincesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ProvinceDto>> GetActiveProvincesAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateProvinceAsync(int id, UpdateProvinceRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeactivateProvinceAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ActivateProvinceAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> DeleteProvinceAsync(int id, CancellationToken cancellationToken = default);
}
