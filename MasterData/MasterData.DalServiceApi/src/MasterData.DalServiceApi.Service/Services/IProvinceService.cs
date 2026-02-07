using MasterData.DalServiceApi.Models.DTOs;

namespace MasterData.DalServiceApi.Service.Services;

/// <summary>
/// رابط سرویس استان - Province Service Interface
/// </summary>
public interface IProvinceService
{
    Task<ProvinceDto> CreateAsync(CreateProvinceDto dto);
    Task<ProvinceDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProvinceDto>> GetAllAsync();
    Task<ProvinceDto> UpdateAsync(int id, UpdateProvinceDto dto);
    Task<bool> DeleteAsync(int id);
}
