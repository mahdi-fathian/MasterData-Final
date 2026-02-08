using MasterData.EventDriven.Application.Interfaces;
using MasterData.EventDriven.Domain.Entities;
using MasterData.EventDriven.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterData.EventDriven.Infrastructure.Repositories;


public class ProvinceRepository : IProvinceRepository
{
    private readonly EventDrivenDbContext _context;

    public ProvinceRepository(EventDrivenDbContext context)
    {
        _context = context;
    }

    public async Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .Where(p => !p.IsDeleted)
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AnyAsync(p => p.Name == name && !p.IsDeleted, cancellationToken);
    }

    public async Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AnyAsync(p => p.Name == name && p.Id != exceptId && !p.IsDeleted, cancellationToken);
    }

    public async Task AddAsync(Province province, CancellationToken cancellationToken = default)
    {
        await _context.Provinces.AddAsync(province, cancellationToken);
    }

    public void Update(Province province)
    {
        _context.Provinces.Update(province);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
