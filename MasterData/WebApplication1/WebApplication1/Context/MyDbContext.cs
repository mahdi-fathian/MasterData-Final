using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Context;

public class MyDbContext : DbContext









{
    public DbSet<Province> Provinces { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Province>().HasIndex(x=>x.Name).IsUnique();
        modelBuilder.Entity<Province>().Property(x => x.Name).HasMaxLength(50);
    }
}









