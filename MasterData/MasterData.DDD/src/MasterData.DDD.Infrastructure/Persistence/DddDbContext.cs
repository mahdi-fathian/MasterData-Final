using MasterData.DDD.Domain.Aggregates.ProvinceAggregate;
using MasterData.DDD.Domain.SeedWork;
using MasterData.DDD.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace MasterData.DDD.Infrastructure.Persistence;

/// <summary>
/// کانتکست دیتابیس DDD - DDD Database Context
/// </summary>
public class DddDbContext : DbContext, IUnitOfWork
{
    public DbSet<Province> Provinces { get; set; } = null!;

    public DddDbContext(DbContextOptions<DddDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Province Aggregate Configuration
        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Provinces");
            entity.HasKey(p => p.Id);

            // Value Object: ProvinceName - با استفاده از OwnsOne
            entity.ComplexProperty(p => p.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(50);
            });

            entity.Property(p => p.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            entity.Property(p => p.CreatedAt)
                .IsRequired();

            entity.Property(p => p.UpdatedAt);

            // Ignore Domain Events (not persisted)
            entity.Ignore(p => p.DomainEvents);
        });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // پردازش رویدادهای دامنه قبل از ذخیره‌سازی
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents != null && e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents!)
            .ToList();

        // در اینجا می‌توانید رویدادهای دامنه را پردازش کنید
        // مثلاً با استفاده از MediatR یا Event Bus

        var result = await base.SaveChangesAsync(cancellationToken);

        // پاک کردن رویدادهای دامنه پس از ذخیره‌سازی
        ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents != null && e.DomainEvents.Any())
            .ToList()
            .ForEach(e => e.ClearDomainEvents());

        return result;
    }
}
