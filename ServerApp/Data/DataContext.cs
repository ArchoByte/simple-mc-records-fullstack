using Microsoft.EntityFrameworkCore;
using SimpleMcRecords.Models;

namespace SimpleMcRecords.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Score> Scores { get; set; } = null!;
    public DbSet<Achievement> Achievements { get; set; } = null!;
    public DbSet<PlayerAchievement> PlayerAchievements { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlayerAchievement>()
            .HasKey(pa => new { pa.PlayerId, pa.AchievementId });
        modelBuilder.Entity<PlayerAchievement>()
            .HasOne(p => p.Player)
            .WithMany(pa => pa.PlayerAchievements)
            .HasForeignKey(p => p.PlayerId);
        modelBuilder.Entity<PlayerAchievement>()
            .HasOne(p => p.Achievement)
            .WithMany(pa => pa.PlayerAchievements)
            .HasForeignKey(a => a.AchievementId);
    }
}