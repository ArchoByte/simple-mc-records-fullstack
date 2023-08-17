using Microsoft.EntityFrameworkCore;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Score> Scores { get; set; } = null!;
    public DbSet<Advancement> Advancements { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<PlayerAdvancement> PlayerAdvancements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerAdvancement>()
            .HasKey(pa => new { pa.PlayerId, pa.AdvancementId });
        modelBuilder.Entity<PlayerAdvancement>()
            .HasOne(p => p.Player)
            .WithMany(pa => pa.PlayerAdvancements)
            .HasForeignKey(p => p.PlayerId);
        modelBuilder.Entity<PlayerAdvancement>()
            .HasOne(p => p.Advancement)
            .WithMany(pa => pa.PlayerAdvancements)
            .HasForeignKey(a => a.AdvancementId);
    }
}