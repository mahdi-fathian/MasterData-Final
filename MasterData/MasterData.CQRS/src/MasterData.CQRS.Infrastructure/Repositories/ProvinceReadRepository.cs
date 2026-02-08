using MasterData.CQRS.Application.Interfaces;
using MasterData.CQRS.Domain.Entities;
using MasterData.CQRS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MasterData.CQRS.Infrastructure.Repositories;


public class ProvinceReadRepository : IProvinceReadRepository
{
    private readonly CqrsDbContext _context;

    public ProvinceReadRepository(CqrsDbContext context)
    {
        _context = context;
    }

    public async Task<Province?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Province>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Provinces
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync(cancellationToken);
    }
}
