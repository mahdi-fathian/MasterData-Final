using MasterData.DalServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterData.DalServiceApi.DAL.Repositories;

public class ProvinceRepository : IProvinceRepository
{
    private readonly AppDbContext _context;

    public ProvinceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Province?> GetByIdAsync(int id)
    {
        return await _context.Provinces.FindAsync(id);
    }

    public async Task<IEnumerable<Province>> GetAllAsync()
    {
        return await _context.Provinces
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Province?> GetByNameAsync(string name)
    {
        return await _context.Provinces
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<Province> AddAsync(Province province)
    {
        province.CreatedAt = DateTime.UtcNow;
        await _context.Provinces.AddAsync(province);
        await _context.SaveChangesAsync();
        return province;
    }

    public async Task<Province> UpdateAsync(Province province)
    {
        province.UpdatedAt = DateTime.UtcNow;
        _context.Provinces.Update(province);
        await _context.SaveChangesAsync();
        return province;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var province = await GetByIdAsync(id);
        if (province == null)
            return false;

        _context.Provinces.Remove(province);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Provinces.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Provinces.AnyAsync(p => p.Name == name);
    }

    public async Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId)
    {
        return await _context.Provinces.AnyAsync(p => p.Name == name && p.Id != exceptId);
    }
}
