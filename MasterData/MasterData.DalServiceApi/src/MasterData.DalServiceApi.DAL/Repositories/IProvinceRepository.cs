using MasterData.DalServiceApi.Models;

namespace MasterData.DalServiceApi.DAL.Repositories;

public interface IProvinceRepository
{
    Task<Province?> GetByIdAsync(int id);
    Task<IEnumerable<Province>> GetAllAsync();
    Task<Province?> GetByNameAsync(string name);
    Task<Province> AddAsync(Province province);
    Task<Province> UpdateAsync(Province province);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId);
}
