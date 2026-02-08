using MasterData.EventDriven.Application.DTOs;

namespace MasterData.EventDriven.Application.Services;

public interface IProvinceService
{
    Task<ProvinceDto> RegisterProvinceAsync(RegisterProvinceRequest request, CancellationToken cancellationToken = default);
    Task<ProvinceDto?> GetProvinceByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProvinceDto>> GetAllProvincesAsync(CancellationToken cancellationToken = default);
    Task<bool> UpdateProvinceAsync(int id, UpdateProvinceRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteProvinceAsync(int id, CancellationToken cancellationToken = default);
}
