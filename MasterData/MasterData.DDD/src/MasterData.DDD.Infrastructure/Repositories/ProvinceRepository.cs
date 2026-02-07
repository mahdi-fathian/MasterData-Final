using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterData.DDD.Infrastructure.Repositories;

/// <summary>
/// پیاده‌سازی مخزن استان - Province Repository Implementation
/// </summary>
public class ProvinceRepository : IProvinceRepository
{
    private readonly DddDbContext _context;

    public ProvinceRepository(DddDbContext context)
    {
        _context = context;
    }

    public async Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Province?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .FirstOrDefaultAsync(p => p.Name.Value == name, cancellationToken);
    }

    public async Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .OrderBy(p => p.Name.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Province>> GetActiveProvincesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .Where(p => p.IsActive)
            .OrderBy(p => p.Name.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AnyAsync(p => p.Name.Value == name, cancellationToken);
    }

    public async Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AnyAsync(p => p.Name.Value == name && p.Id != exceptId, cancellationToken);
    }

    public async Task AddAsync(Province province, CancellationToken cancellationToken = default)
    {
        await _context.Provinces.AddAsync(province, cancellationToken);
    }

    public void Update(Province province)
    {
        _context.Provinces.Update(province);
    }

    public void Remove(Province province)
    {
        _context.Provinces.Remove(province);
    }
}
