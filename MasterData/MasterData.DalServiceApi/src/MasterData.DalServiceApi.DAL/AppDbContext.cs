using MasterData.DalServiceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterData.DalServiceApi.DAL;

/// <summary>
/// کانتکست دیتابیس - Database Context
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Province> Provinces { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
        });
    }
}
