using MasterData.CQRS.Application.Interfaces;
using MasterData.CQRS.Domain.Entities;
using MasterData.CQRS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterData.CQRS.Infrastructure.Repositories;


public class ProvinceRepository : IProvinceRepository
{
    private readonly CqrsDbContext _context;

    public ProvinceRepository(CqrsDbContext context)
    {
        _context = context;
    }

    public async Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AnyAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsByNameExceptIdAsync(string name, int exceptId, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AnyAsync(p => p.Name == name && p.Id != exceptId, cancellationToken);
    }

    public async Task AddAsync(Province province, CancellationToken cancellationToken = default)
    {
        await _context.Provinces.AddAsync(province, cancellationToken);
    }

    public void Update(Province province)
    {
        _context.Provinces.Update(province);
    }

    public void Delete(Province province)
    {
        _context.Provinces.Remove(province);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
