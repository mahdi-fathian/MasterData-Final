using MasterData.EventDriven.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MasterData.EventDriven.Infrastructure.Persistence;

public class EventDrivenDbContext : DbContext
{
    public DbSet<Province> Provinces { get; set; } = null!;
    public DbSet<EventStore> EventStores { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;

    public EventDrivenDbContext(DbContextOptions<EventDrivenDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Province Configuration
        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Provinces");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("IX_Provinces_Name");

            entity.Property(p => p.CreatedAt)
                .IsRequired();

            entity.Property(p => p.UpdatedAt);

            entity.Property(p => p.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Ignore Events collection (not persisted)
            entity.Ignore(p => p.Events);
        });

        // EventStore Configuration
        modelBuilder.Entity<EventStore>(entity =>
        {
            entity.ToTable("EventStores");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.AggregateId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.AggregateType)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.EventType)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.EventData)
                .IsRequired();

            entity.Property(e => e.Version)
                .IsRequired();

            entity.Property(e => e.OccurredOn)
                .IsRequired();

            entity.HasIndex(e => new { e.AggregateId, e.Version })
                .IsUnique()
                .HasDatabaseName("IX_EventStores_AggregateId_Version");
        });

        // OutboxMessage Configuration
        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.ToTable("OutboxMessages");
            entity.HasKey(o => o.Id);

            entity.Property(o => o.EventType)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(o => o.EventData)
                .IsRequired();

            entity.Property(o => o.OccurredOn)
                .IsRequired();

            entity.Property(o => o.IsProcessed)
                .IsRequired()
                .HasDefaultValue(false);

            entity.Property(o => o.RetryCount)
                .IsRequired()
                .HasDefaultValue(0);

            entity.HasIndex(o => o.IsProcessed)
                .HasDatabaseName("IX_OutboxMessages_IsProcessed");

            entity.HasIndex(o => o.OccurredOn)
                .HasDatabaseName("IX_OutboxMessages_OccurredOn");
        });
    }
}
