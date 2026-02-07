namespace MasterData.DDD.Domain.SeedWork;

/// <summary>
/// رابط واحد کار - Unit of Work Interface
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
